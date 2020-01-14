using Common.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace WPF.Converters
{
    class ItemGenreCollectionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var itemGenreCollection = (IEnumerable<ItemGenre>)value;
            var stringBuilder = new StringBuilder();
            foreach (var item in itemGenreCollection)
            {
                stringBuilder.Append(item.Genre.Name);
                stringBuilder.Append(" ");
            }
            return stringBuilder.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
