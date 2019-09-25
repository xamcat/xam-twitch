using System;
using Xamarin.Forms;

namespace XamTwitch.Controls
{
    public class PlayerView : View
    {
        public static BindableProperty SourceProperty = BindableProperty.Create("Source", typeof(string), typeof(PlayerView));

        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public PlayerView()
        {
            
        }
    }
}
