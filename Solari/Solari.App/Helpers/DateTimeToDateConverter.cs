using Microsoft.UI.Xaml.Data;
using System;
using System.Globalization;

namespace Solari.App.Helpers
{
    /// <summary>
    /// A helper for converting DateTime to a date string (without time).
    /// </summary>
    public class DateTimeToDateConverter : IValueConverter
    {
        public static object Convert(object value, Type targetType, object parameter, CultureInfo culture)
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
