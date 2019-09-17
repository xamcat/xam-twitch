using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamTwitch.Controls;
using XamTwitch.Droid.CustomerRenderers;
using ARelativeLayout = Android.Widget.RelativeLayout;

[assembly: ExportRenderer(typeof(PlayerView), typeof(PlayerViewRenderer))]

namespace XamTwitch.Droid.CustomerRenderers
{
    public class PlayerViewRenderer : ViewRenderer<PlayerView, ARelativeLayout>
    {
        private VideoView _videoView;
        private MediaController _mediaController;

        public PlayerViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<PlayerView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // TODO: cleanup
            }

            if (e.NewElement != null)
            {
                _videoView = new VideoView(Context);
                var relativeLayout = new ARelativeLayout(Context);
                //var tmpControl = new TextView(Context)
                //{
                //    Text = "My Custom Video Player Here",
                //    Background = new ColorDrawable(Android.Graphics.Color.Red),
                //    TextSize = 64f,
                //};
                //relativeLayout.AddView(tmpControl);
                relativeLayout.AddView(_videoView);

                var layoutParams = new ARelativeLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
                layoutParams.AddRule(LayoutRules.CenterInParent);
                //tmpControl.LayoutParameters = layoutParams;

                _videoView.LayoutParameters = layoutParams;

                _mediaController = new MediaController(Context);
                _mediaController.SetMediaPlayer(_videoView);
                _videoView.SetMediaController(_mediaController);
               
                SetSource();
                this.SetNativeControl(relativeLayout);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Element.Source))
            {
                SetSource();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        private void SetSource()
        {
            if (string.IsNullOrWhiteSpace(this.Element.Source))
                return;

            var androidUri = Android.Net.Uri.Parse(this.Element.Source);
            _videoView.SetVideoURI(androidUri);
            _videoView.Start();

            System.Diagnostics.Debug.WriteLine($"PlayerViewRenderer.SetSource.Play: {this.Element.Source}");
        }
    }
}
