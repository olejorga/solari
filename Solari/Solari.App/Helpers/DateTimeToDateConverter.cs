using Microsoft.UI.Xaml.Data;
using System;
using System.Globalization;

namespace Solari.App.Helpers
{
    public class DateTimeToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Split(" ")[0];
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value.ToString().Split(" ")[0];
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
