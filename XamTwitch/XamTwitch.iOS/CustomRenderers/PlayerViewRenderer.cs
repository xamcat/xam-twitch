using System;
using System.ComponentModel;
using AVFoundation;
using AVKit;
using Foundation;
using UIKit;
using Xamarin.Essentials;
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
                ClearNativePlayer();
            }

            if (e.NewElement != null)
            {
                System.Diagnostics.Debug.WriteLine($"PlayerViewRenderer.OnElementChanged.NewElement is not null");

                if (Control == null)
                {
                    SetSource();
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
            ClearNativePlayer();
            CreateNativePlayer();

            _player?.Play();
            System.Diagnostics.Debug.WriteLine($"PlayerViewRenderer.SetSource.Play ({MainThread.IsMainThread}): {this.Element.Source}");
        }

        private void ClearNativePlayer()
        {
            if (_player != null)
            {
                _player.Pause();
                _player.Dispose();
                _player = null;
            }
        }

        private void CreateNativePlayer()
        {
            var source = this.Element.Source;
            if (string.IsNullOrWhiteSpace(source))
                return;

            var asset = AVAsset.FromUrl(new NSUrl(source));
            var item = new AVPlayerItem(asset);
            var playerViewController = new AVPlayerViewController();
            _player = new AVPlayer(item);
            playerViewController.Player = _player;
            this.SetNativeControl(playerViewController.View);
        }
    }
}
