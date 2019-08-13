using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamTwitch.Converters
{
    public class UrlFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var urlTest = value.ToString();
            return urlTest.Replace("{width}x{height}", "400x200");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
