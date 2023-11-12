using Standard.Tool.Platform.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Standard.Tool.Platform.Common.Converters;

public class VisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        Visibility sRet = Visibility.Visible;
        if (value != null)
        {
            sRet = (bool)value ? Visibility.Visible : Visibility.Hidden;
        }
        return sRet;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
