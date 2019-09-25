using System;
using System.ComponentModel;
using AVFoundation;
using AVKit;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamTwitch.Controls;
using XamTwitch.iOS.CustomRenderers;

[assembly:ExportRenderer(typeof(PlayerView), typeof(PlayerViewRenderer))]

namespace XamTwitch.iOS.CustomRenderers
{
    public class PlayerViewRenderer : ViewRenderer<PlayerView, UIView>
    {
        private AVPlayer _player;

        public PlayerViewRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<PlayerView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                System.Diagnostics.Debug.WriteLine($"PlayerViewRenderer.OnElementChanged.OldElement is not null");
                if (_player != null)
                {
                    _player.Pause();
                    _player.Dispose();
                    _player = null;
                }
            }

            if (e.NewElement != null)
            {
                System.Diagnostics.Debug.WriteLine($"PlayerViewRenderer.OnElementChanged.NewElement is not null");

                if (Control == null)
                {
                    var playerViewController = new AVPlayerViewController();
                    _player = new AVPlayer();
                    playerViewController.Player = _player;

                    SetSource();
                    this.SetNativeControl(playerViewController.View);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(Element.Source))
            {
                SetSource();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        private void SetSource()
        {
            if(string.IsNullOrWhiteSpace(this.Element.Source))
                return;
    
            var asset = AVAsset.FromUrl(new NSUrl(this.Element.Source));
            var item = new AVPlayerItem(asset);
            _player.ReplaceCurrentItemWithPlayerItem(item);
            _player.Play();

            System.Diagnostics.Debug.WriteLine($"PlayerViewRenderer.SetSource.Play: {this.Element.Source}");
        }
    }
}
