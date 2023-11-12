using MahApps.Metro.IconPacks;
using Standard.Tool.Platform.Library.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Standard.Tool.Platform.Library.Converters
{
    public class ToastIconConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            object value = values[0];
            object grid = values[1];
            object txt = values[2];
            Grid _grid = grid as Grid;
            TextBlock _txt = txt as TextBlock;
            if (value == null)
            {
                if (_grid != null)
                {
                    _grid.ColumnDefinitions.RemoveAt(0);
                }
                if (_txt != null)
                {
                    _txt.HorizontalAlignment = HorizontalAlignment.Center;
                }
                return PackIconFontAwesomeKind.None;
            }
            EnumToastType _value;
            try
            {
                _value = (EnumToastType)value;
            }
            catch
            {
                if (_grid != null)
                {
                    _grid.ColumnDefinitions.RemoveAt(0);
                }
                if (_txt != null)
                {
                    _txt.HorizontalAlignment = HorizontalAlignment.Center;
                }
                return PackIconFontAwesomeKind.None;
            }
            switch (_value)
            {
                case EnumToastType.Information:
                    return PackIconFontAwesomeKind.CheckSolid;
                case EnumToastType.Error:
                    return PackIconFontAwesomeKind.TimesSolid;
                case EnumToastType.Warning:
                    return PackIconFontAwesomeKind.ExclamationSolid;
                case EnumToastType.Busy:
                    return PackIconFontAwesomeKind.ClockSolid;
            }
            if (_grid != null)
            {
                _grid.ColumnDefinitions.RemoveAt(0);
            }
            if (_txt != null)
            {
                _txt.HorizontalAlignment = HorizontalAlignment.Center;
            }
            return PackIconFontAwesomeKind.None;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
