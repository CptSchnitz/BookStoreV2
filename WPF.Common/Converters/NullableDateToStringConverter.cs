using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace WPF.Common.Converters
{
    public class NullableDateToStringConverter : IValueConverter
    {
        private readonly string format = "dd.MM.yyyy";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime? date = value as DateTime?;
            if (date != null)
            {
                return date.Value.ToString(format);
            }
            else
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateString = value as string;
            if (dateString != null || string.IsNullOrWhiteSpace(dateString))
            {
                DateTime result;
                if (DateTime.TryParseExact(dateString, format,null ,DateTimeStyles.None, out result))
                {
                    return result;
                }
            }
            return null;
        }
    }
}
