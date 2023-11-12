using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Standard.Tool.Platform.Library.Converters
{
    public class FindResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Application.Current.TryFindResource(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
