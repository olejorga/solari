using Microsoft.UI.Xaml.Data;
using System;
using System.Globalization;

namespace Solari.App.Helpers
{
    public class TimeSpanToClockConverter : IValueConverter
    {
        public static object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] hands = value.ToString().Split(":");
            return $"{hands[0]}:{hands[1]}";
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string[] hands = value.ToString().Split(":");
            return $"{hands[0]}:{hands[1]}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
