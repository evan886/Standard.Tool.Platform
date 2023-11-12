using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;

namespace Standard.Tool.Platform.Library
{
    [TemplatePart(Name = DecreaseButtonTemplateName, Type = typeof(Button))]
    [TemplatePart(Name = IncreaseButtonTemplateName, Type = typeof(Button))]
    [TemplatePart(Name = NumberTextBoxTemplateName, Type = typeof(TextBox))]
    public class InputNumber : Control
    {
        static InputNumber()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InputNumber), new FrameworkPropertyMetadata(typeof(InputNumber)));
        }

        private const string DecreaseButtonTemplateName = "PART_DecreaseButton";
        private const string IncreaseButtonTemplateName = "PART_IncreaseButton";
        private const string NumberTextBoxTemplateName = "PART_NumberTextBox";

        private Button _decreaseButton;
        private Button _increaseButton;
        private TextBox _numberTextBox;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _decreaseButton = GetTemplateChild(DecreaseButtonTemplateName) as Button;
            _increaseButton = GetTemplateChild(IncreaseButtonTemplateName) as Button;
            _numberTextBox = GetTemplateChild(NumberTextBoxTemplateName) as TextBox;
            if (_decreaseButton != null)
            {
                _decreaseButton.Click += _decreaseButton_Click;
            }
            if (_increaseButton != null)
            {
                _increaseButton.Click += _increaseButton_Click;
            }


        }

        private void _increaseButton_Click(object sender, RoutedEventArgs e)
        {
            Number = (Number ?? 0) + Step;
        }

        private void _decreaseButton_Click(object sender, RoutedEventArgs e)
        {
            Number = (Number ?? 0) - Step;
        }

        protected override void OnTemplateChanged(ControlTemplate oldTemplate, ControlTemplate newTemplate)
        {
            base.OnTemplateChanged(oldTemplate, newTemplate);
        }

        public new bool Focus()
        {
            return _numberTextBox.Focus();
        }

        #region 设置计数器允许的最小值



        public double Min
        {
            get { return (double)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Min.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinProperty =
            DependencyProperty.Register("Min", typeof(double), typeof(InputNumber), new PropertyMetadata(double.MinValue));
        #endregion

        #region  设置计数器允许的最大值
        public double Max
        {
            get { return (double)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Max.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxProperty =
            DependencyProperty.Register("Max", typeof(double), typeof(InputNumber), new PropertyMetadata(double.MaxValue));
        #endregion

        #region 计数器步长
        public double Step
        {
            get { return (double)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Step.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(double), typeof(InputNumber), new PropertyMetadata(1.0));
        #endregion

        #region 是否只能输入 step 的倍数


        public bool StepStrictly
        {
            get { return (bool)GetValue(StepStrictlyProperty); }
            set { SetValue(StepStrictlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StepStrictly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepStrictlyProperty =
            DependencyProperty.Register("StepStrictly", typeof(bool), typeof(InputNumber), new PropertyMetadata(false));


        #endregion

        #region 数值精度 --  precision 的值必须是一个非负整数，并且不能小于 step 的小数位数。


        public int Precision
        {
            get { return (int)GetValue(PrecisionProperty); }
            set { SetValue(PrecisionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Precision.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PrecisionProperty =
            DependencyProperty.Register("Precision", typeof(int), typeof(InputNumber), new PropertyMetadata(0));


        #endregion

        #region 是否使用控制按钮


        public bool Controls
        {
            get { return (bool)GetValue(ControlsProperty); }
            set { SetValue(ControlsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Controls.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControlsProperty =
            DependencyProperty.Register("Controls", typeof(bool), typeof(InputNumber), new PropertyMetadata(true));


        #endregion

        #region 控制按钮位置



        public EnumPosition ControlsPosition
        {
            get { return (EnumPosition)GetValue(ControlsPositionProperty); }
            set { SetValue(ControlsPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ControlsPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControlsPositionProperty =
            DependencyProperty.Register("ControlsPosition", typeof(EnumPosition), typeof(InputNumber), new PropertyMetadata(EnumPosition.Default));


        #endregion

        #region 输入框默认 placeholder


        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlaceHolder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceHolderProperty =
            DependencyProperty.Register("PlaceHolder", typeof(string), typeof(InputNumber), new PropertyMetadata(string.Empty));


        #endregion

        #region 计数器的值
        public double? Number
        {
            get { return (double?)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Number.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumberProperty =
            DependencyProperty.Register("Number", typeof(double?), typeof(InputNumber), new PropertyMetadata(null, NumberPropertyChanged));

        private static void NumberPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is InputNumber number)
            {
                double max = number.Max;
                double min = number.Min;
                number._decreaseButton.IsEnabled = (double)e.NewValue > min;
                number._increaseButton.IsEnabled = (double)e.NewValue < max;
            };
        }
        #endregion

        #region 表示矩形的角的半径。


        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(InputNumber), new PropertyMetadata(new CornerRadius(0)));


        #endregion
    }

    public enum EnumPosition
    {
        Default,
        Left,
        Right,
    }

    public class NumberToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
