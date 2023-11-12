using NPOI.SS.Formula.Functions;
using Standard.Tool.Platform.Library;
using Standard.Tool.Platform.Library.Enums;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using MediaDraw= System.Windows.Media;
using System.Windows.Threading;

namespace Standard.Tool.Platform.UserControls.Toast
{
    /// <summary>
    /// ToastControlNotification.xaml 的交互逻辑
    /// </summary>
    public partial class ToastControlNotification : UserControl
    {
        private Window owner = null;
        private Popup popup = null;
        private DispatcherTimer timer = null;

        private ToastControlNotification()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private ToastControlNotification(Window owner, string message, ToastOptions options = null)
        {
            Message = message;
            InitializeComponent();
            if (options != null)
            {
                Icon = options.Icon;
                Location = options.Location;
                Time = options.Time;
                Closed += options.Closed;
                Click += options.Click;
                HorizontalContentAlignment = options.HorizontalContentAlignment;
            }
            this.DataContext = this;
            if (owner == null)
            {
                this.owner = Application.Current.MainWindow;
       
            }
            else
            {
                this.owner = owner;
            }
            this.owner.Closed += Owner_Closed;
        }

        private void Owner_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        public static void Show(string msg, ToastOptions options = null)
        {
            var toast = new ToastControlNotification(null, msg, options);
            int time = toast.Time;
            ShowToast(toast, time);
        }

        public static void Show(Window owner, string msg, ToastOptions options = null)
        {
            var toast = new ToastControlNotification(owner, msg, options);
            int time = toast.Time;
            ShowToast(toast, time);
        }

        private static void ShowToast(ToastControlNotification toast, int time)
        {
            toast.popup = null;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                toast.popup = new Popup
                {
                    PopupAnimation = PopupAnimation.Fade,
                    AllowsTransparency = true,
                    StaysOpen = true,
                    Placement = PlacementMode.Left,
                    IsOpen = false,
                    Child = toast,
                    MinWidth = toast.MinWidth,
                    MaxWidth = toast.MaxWidth,
                    MinHeight = toast.MinHeight,
                    MaxHeight = toast.MaxHeight,
                };

                toast.popup.PlacementTarget = GetPopupPlacementTarget(toast); //为 null 则 Popup 定位相对于屏幕的左上角;
                toast.owner.LocationChanged += toast.UpdatePosition;
                toast.owner.SizeChanged += toast.UpdatePosition;
                toast.popup.Closed += Popup_Closed;

                //SetPopupOffset(toast.popup, toast);
                //toast.UpdatePosition(toast, null);
                toast.popup.IsOpen = true;  //先显示出来以确定宽高；
                SetPopupOffset(toast.popup, toast);
                //toast.UpdatePosition(toast, null);
                toast.popup.IsOpen = false; //先关闭再打开来刷新定位；
                toast.popup.IsOpen = true;
            }));
            toast.timer = new DispatcherTimer();
            toast.timer.Tick += (sender, e) =>
            {
                toast.popup.IsOpen = false;
                toast.owner.LocationChanged -= toast.UpdatePosition;
                toast.owner.SizeChanged -= toast.UpdatePosition;
            };
            toast.timer.Interval = new TimeSpan(0, 0, 0, 0, time);
            toast.timer.Start();
        }

        private void UpdatePosition(object sender, EventArgs e)
        {
            var up = typeof(Popup).GetMethod("UpdatePosition", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (up == null || popup == null)
            {
                return;
            }
            SetPopupOffset(popup, this);
            up.Invoke(popup, null);
        }

        private static void Popup_Closed(object sender, EventArgs e)
        {
            Popup popup = sender as Popup;
            if (popup == null)
            {
                return;
            }
            ToastControlNotification toast = popup.Child as ToastControlNotification;
            if (toast == null)
            {
                return;
            }
            toast.RaiseClosed(e);
        }

        private void UserControl_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                RaiseClick(e);
            }
        }

        /// <summary>
        /// 获取定位目标
        /// </summary>
        /// <param name="toast">Toast 对象</param>
        /// <returns>容器或null</returns>
        private static UIElement GetPopupPlacementTarget(ToastControlNotification toast)
        {
            switch (toast.Location)
            {
                case EnumToastLocation.ScreenCenter:
                case EnumToastLocation.ScreenLeft:
                case EnumToastLocation.ScreenRight:
                case EnumToastLocation.ScreenTopLeft:
                case EnumToastLocation.ScreenTopCenter:
                case EnumToastLocation.ScreenTopRight:
                case EnumToastLocation.ScreenBottomLeft:
                case EnumToastLocation.ScreenBottomCenter:
                case EnumToastLocation.ScreenBottomRight:
                    return null;
            }
            return toast.owner;
        }

        private static void SetPopupOffset(Popup popup, ToastControlNotification toast)
        {
            double winTitleHeight = SystemParameters.CaptionHeight; //标题高度为22；
            double owner_width = toast.owner.ActualWidth;
            double owner_height = toast.owner.ActualHeight - winTitleHeight;
            if (popup.PlacementTarget == null)
            {
                owner_width = SystemParameters.WorkArea.Size.Width;
                owner_height = SystemParameters.WorkArea.Size.Height;
            }

            double popupWidth = (popup.Child as FrameworkElement)?.ActualWidth ?? 0; //Popup 宽高为其 Child 的宽高；
            double popupHeight = (popup.Child as FrameworkElement)?.ActualHeight ?? 0;
            double x = SystemParameters.WorkArea.X;
            double y = SystemParameters.WorkArea.Y;
            Thickness margin = toast.ToastMargin;

            /*[dlgcy] 38 和 16 两个数字的猜测：
             * PlacementTarget 为 Window 时，当 Placement 为 Bottom 时，Popup 上边缘与 Window 的下边缘的距离为 38；
             * 当 Placement 为 Right 时，Popup 左边缘与 Window 的右边缘的距离为 16。
             */
            double bottomDistance = 38;
            double rightDistance = 16;

            //上面创建时 Popup 的 Placement 为 PlacementMode.Left；
            switch (toast.Location)
            {
                case EnumToastLocation.OwnerLeft: //容器左中间
                    popup.HorizontalOffset = popupWidth + margin.Left;
                    popup.VerticalOffset = (owner_height - popupHeight - winTitleHeight) / 2;
                    break;
                case EnumToastLocation.ScreenLeft: //屏幕左中间
                    popup.HorizontalOffset = popupWidth + x + margin.Left;
                    popup.VerticalOffset = (owner_height - popupHeight) / 2 + y;
                    break;
                case EnumToastLocation.OwnerRight: //容器右中间
                    popup.HorizontalOffset = owner_width - rightDistance - margin.Right;
                    popup.VerticalOffset = (owner_height - popupHeight - winTitleHeight) / 2;
                    break;
                case EnumToastLocation.ScreenRight: //屏幕右中间
                    popup.HorizontalOffset = owner_width + x - margin.Right;
                    popup.VerticalOffset = (owner_height - popupHeight) / 2 + y;
                    break;
                case EnumToastLocation.OwnerTopLeft: //容器左上角
                    popup.HorizontalOffset = popupWidth + margin.Left;
                    popup.VerticalOffset = margin.Top;
                    break;
                case EnumToastLocation.ScreenTopLeft: //屏幕左上角
                    popup.HorizontalOffset = popupWidth + x + margin.Left;
                    popup.VerticalOffset = margin.Top;
                    break;
                case EnumToastLocation.OwnerTopCenter: //容器上中间
                    popup.HorizontalOffset = popupWidth + (owner_width - popupWidth - rightDistance) / 2;
                    popup.VerticalOffset = margin.Top;
                    break;
                case EnumToastLocation.ScreenTopCenter: //屏幕上中间
                    popup.HorizontalOffset = popupWidth + (owner_width - popupWidth) / 2 + x;
                    popup.VerticalOffset = y + margin.Top;
                    break;
                case EnumToastLocation.OwnerTopRight: //容器右上角
                    popup.HorizontalOffset = owner_width - rightDistance - margin.Right;
                    popup.VerticalOffset = margin.Top;
                    break;
                case EnumToastLocation.ScreenTopRight: //屏幕右上角
                    popup.HorizontalOffset = owner_width + x - margin.Right;
                    popup.VerticalOffset = y + margin.Top;
                    break;
                case EnumToastLocation.OwnerBottomLeft: //容器左下角
                    popup.Placement = PlacementMode.Bottom;
                    popup.HorizontalOffset = margin.Left;
                    popup.VerticalOffset = -(bottomDistance + popupHeight + margin.Bottom);
                    break;
                case EnumToastLocation.ScreenBottomLeft: //屏幕左下角
                    popup.HorizontalOffset = popupWidth + x + margin.Left;
                    popup.VerticalOffset = owner_height - popupHeight + y - margin.Bottom;
                    break;
                case EnumToastLocation.OwnerBottomCenter: //容器下中间
                    popup.Placement = PlacementMode.Bottom;
                    popup.HorizontalOffset = (owner_width - popupWidth - rightDistance) / 2;
                    popup.VerticalOffset = -(bottomDistance + popupHeight + margin.Bottom);
                    break;
                case EnumToastLocation.ScreenBottomCenter: //屏幕下中间
                    popup.HorizontalOffset = popupWidth + (owner_width - popupWidth) / 2 + x;
                    popup.VerticalOffset = owner_height - popupHeight + y - margin.Bottom;
                    break;
                case EnumToastLocation.OwnerBottomRight: //容器右下角
                    popup.Placement = PlacementMode.Bottom;
                    popup.HorizontalOffset = owner_width - popupWidth - rightDistance - margin.Right;
                    popup.VerticalOffset = -(bottomDistance + popupHeight + margin.Bottom);
                    break;
                case EnumToastLocation.ScreenBottomRight: //屏幕右下角
                    popup.HorizontalOffset = owner_width + x - margin.Right;
                    popup.VerticalOffset = owner_height - popupHeight + y - margin.Bottom;
                    break;
                case EnumToastLocation.ScreenCenter: //屏幕正中间
                    popup.HorizontalOffset = popupWidth + (owner_width - popupWidth) / 2 + x;
                    popup.VerticalOffset = (owner_height - popupHeight) / 2 + y;
                    break;
                case EnumToastLocation.OwnerCenter: //容器正中间
                case EnumToastLocation.Default:
                    popup.Placement = PlacementMode.Center;
                    popup.HorizontalOffset = -rightDistance / 2;
                    popup.VerticalOffset = -bottomDistance / 2;
                    break;
            }
        }

        public void Close()
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }
            popup.IsOpen = false;
            owner.LocationChanged -= UpdatePosition;
            owner.SizeChanged -= UpdatePosition;
        }

        private event EventHandler<EventArgs> Closed;
        private void RaiseClosed(EventArgs e)
        {
            Closed?.Invoke(this, e);
        }

        private event EventHandler<EventArgs> Click;
        private void RaiseClick(EventArgs e)
        {
            Click?.Invoke(this, e);
        }

        #region 依赖属性

        private string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        private static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(ToastControlNotification), new PropertyMetadata(string.Empty));

        
        private EnumToastType Icon
        {
            get { return (EnumToastType)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        private static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(EnumToastType), typeof(ToastControlNotification), new PropertyMetadata(EnumToastType.None));

        private int Time
        {
            get { return (int)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }
        private static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(int), typeof(ToastControlNotification), new PropertyMetadata(2000));

        private EnumToastLocation Location
        {
            get { return (EnumToastLocation)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }
        private static readonly DependencyProperty LocationProperty =
            DependencyProperty.Register("Location", typeof(EnumToastLocation), typeof(ToastControlNotification), new PropertyMetadata(EnumToastLocation.Default));

        public Thickness ToastMargin
        {
            get { return (Thickness)GetValue(ToastMarginProperty); }
            set { SetValue(ToastMarginProperty, value); }
        }
        public static readonly DependencyProperty ToastMarginProperty =
            DependencyProperty.Register("ToastMargin", typeof(Thickness), typeof(ToastControlNotification), new PropertyMetadata(new Thickness(0)));

        private MediaDraw.Brush IconForeground
        {
            get { return (MediaDraw.Brush)GetValue(IconForegroundProperty); }
            set { SetValue(IconForegroundProperty, value); }
        }
        private static readonly DependencyProperty IconForegroundProperty =
            DependencyProperty.Register("IconForeground", typeof(MediaDraw.Brush), typeof(ToastControlNotification), new PropertyMetadata((MediaDraw.Brush)new MediaDraw.BrushConverter().ConvertFromString("#00D91A")));

        #endregion
    }
}
