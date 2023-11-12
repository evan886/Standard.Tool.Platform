using Standard.Tool.Platform.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Standard.Tool.Platform.Common.Converters;

public class AccountStatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        string sRet = "";
        if (value != null)
        {
            sRet = value.ToString().ToLower() switch
            {
                "enable" => string.Format("启用"),
                "disable" => string.Format("禁用"),
                _ => throw new NotFoundException("Invalide status types,")
            };
        }
        return sRet;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
