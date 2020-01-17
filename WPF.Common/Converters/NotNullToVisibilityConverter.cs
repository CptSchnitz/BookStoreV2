using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPF.Common.Converters
{
    // a converter to bind visibility to an object. if the object is null the visibility is hidden.
    public class NotNullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
