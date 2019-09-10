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
        private AVPlayerViewController _playerViewController;
        private AVPlayer _player;

        public PlayerViewRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<PlayerView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    _playerViewController = new AVPlayerViewController();   
                    _player = new AVPlayer();
                    _playerViewController.Player = _player;

                    SetSource();
                    this.SetNativeControl(_playerViewController.View);
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
        }
    }
}
