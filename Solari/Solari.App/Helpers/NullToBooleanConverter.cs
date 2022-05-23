using Microsoft.UI.Xaml.Data;
using System;
using System.Globalization;

namespace Solari.App.Helpers
{
    /// <summary>
    /// A helper for converting null to WINUI boolean ("True") and ("False").
    /// </summary>
    public class NullToBooleanConverter : IValueConverter
    {
        public static object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? "False" : "True";
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value == null ? "False" : "True";
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
