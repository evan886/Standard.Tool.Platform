using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using Standard.Tool.Platform.Library.Enums;

namespace Standard.Tool.Platform.Library
{
    public class ToastOptions
    {
        public int Time { get; set; } = 2000;
        public EnumToastType Icon { get; set; } = EnumToastType.None;
        public EnumToastLocation Location { get; set; } = EnumToastLocation.Default;
        public HorizontalAlignment HorizontalContentAlignment { get; set; } = HorizontalAlignment.Left;
        public EventHandler<EventArgs> Closed { get; internal set; }
        public EventHandler<EventArgs> Click { get; internal set; }
    }

}
