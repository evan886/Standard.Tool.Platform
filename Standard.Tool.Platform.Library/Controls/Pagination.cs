﻿using System;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows;

namespace Standard.Tool.Platform.Library
{
    [TemplatePart(Name = "PrevButtonTemplateName", Type = typeof(Button))]
    [TemplatePart(Name = "NextButtonTemplateName", Type = typeof(Button))]
    public class Pagination : Selector
    {
        private const string PrevButtonTemplateName = "PART_PREV";
        private const string NextButtonTemplateName = "PART_Next";
        private const string PageItemsTemplateName = "PART_PAGE";

        private Button _prevButton;
        private Button _nextButton;

        #region PageChanging事件
        public static readonly RoutedEvent PageChangedEvent = EventManager.RegisterRoutedEvent("PageChangedEvent", RoutingStrategy.Direct, typeof(RoutedPropertyChangedEventArgs<int>), typeof(Pagination));

        public event RoutedEventHandler PageChanged
        {
            //将路由事件添加路由事件处理程序
            add { AddHandler(PageChangedEvent, value); }
            //从路由事件处理程序中移除路由事件
            remove { RemoveHandler(PageChangedEvent, value); }
        }
        #endregion


        public override void OnApplyTemplate()
        {
            if (_prevButton != null)
            {
                _prevButton.Click -= _prevButton_Click;
            }
            if (_nextButton != null)
            {
                _nextButton.Click -= _nextButton_Click;
            }
            base.OnApplyTemplate();
            _prevButton = GetTemplateChild(PrevButtonTemplateName) as Button;
            _nextButton = GetTemplateChild(NextButtonTemplateName) as Button;
            if (_prevButton != null)
            {
                _prevButton.Click += _prevButton_Click;
            }
            if (_nextButton != null)
            {
                _nextButton.Click += _nextButton_Click;
            }
            InitPager();
        }

        private void _nextButton_Click(object sender, RoutedEventArgs e)
        {
            SetCurrentValue(CurrentPageProperty, CurrentPage + 1);
        }

        private void _prevButton_Click(object sender, RoutedEventArgs e)
        {
            SetCurrentValue(CurrentPageProperty, CurrentPage - 1);
        }

        static Pagination()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pagination), new FrameworkPropertyMetadata(typeof(Pagination)));

            ItemsPanelTemplate template = new ItemsPanelTemplate(new FrameworkElementFactory(typeof(WrapPanel)));
            template.Seal();
            ItemsPanelProperty.OverrideMetadata(typeof(Pagination), new FrameworkPropertyMetadata(template));
        }

        #region 每页显示条目个数
        /// <summary>
        /// 每页显示条目个数
        /// </summary>
        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize", typeof(int), typeof(Pagination), new PropertyMetadata(10, InitPagination));

        #endregion

        #region 总页数
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get { return (int)GetValue(PageCountProperty); }
            set { SetValue(PageCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageCountProperty =
            DependencyProperty.Register("PageCount", typeof(int), typeof(Pagination), new PropertyMetadata(0, InitPagination));
        #endregion

        #region 总条目数
        /// <summary>
        /// 总条目数
        /// </summary>
        public int Total
        {
            get { return (int)GetValue(TotalProperty); }
            set { SetValue(TotalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Total.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalProperty =
            DependencyProperty.Register("Total", typeof(int), typeof(Pagination), new PropertyMetadata(0, InitPagination));
        #endregion

        #region 当前页数
        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurrentPage
        {
            get { return (int)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage", typeof(int), typeof(Pagination), new PropertyMetadata(1, InitPagination));
        #endregion

        #region 页码按钮的数量，当总页数超过该值时会折叠  大于等于 5 且小于等于 21 的奇数


        public int PagerCount
        {
            get { return (int)GetValue(PagerCountProperty); }
            set { SetValue(PagerCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PagerCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PagerCountProperty =
            DependencyProperty.Register("PagerCount", typeof(int), typeof(Pagination), new PropertyMetadata(5, InitPagination), OnPagerCountPropertyValidate);

        private static bool OnPagerCountPropertyValidate(object value)
        {
            return int.TryParse(value?.ToString(), out int num) && (num & 1) != 0 && num >= 5 && num <= 21;
        }


        #endregion

        #region 只有一页时是否隐藏


        public bool HideOnSinglePage
        {
            get { return (bool)GetValue(HideOnSinglePageProperty); }
            set { SetValue(HideOnSinglePageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HideOnSinglePage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HideOnSinglePageProperty =
            DependencyProperty.Register("HideOnSinglePage", typeof(bool), typeof(Pagination), new PropertyMetadata(true, HideOnSinglePagePropertyChanged));

        private static void HideOnSinglePagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Pagination pagination)
            {
                pagination.SetCurrentValue(VisibilityProperty, (pagination.PageCount == 1 && (bool)e.NewValue) ? Visibility.Collapsed : Visibility.Visible);
            }
        }


        #endregion

        private void InitPager()
        {
            var pageCount = (int)Math.Ceiling((double)Total / PageSize);
            if (PageCount == 1 && HideOnSinglePage)
            {
                SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
                return;
            }
            else
            {
                SetCurrentValue(VisibilityProperty, Visibility.Visible);
            }

            var currentPage = CurrentPage;
            var pagerCount = PagerCount;

            var startPageIndex = currentPage - pagerCount / 2;
            startPageIndex = startPageIndex < 1 ? 1 : startPageIndex;

            var endPageIndex = startPageIndex + pagerCount - 1;
            endPageIndex = endPageIndex > pageCount ? pageCount : endPageIndex;

            startPageIndex = endPageIndex - pagerCount + 1;
            startPageIndex = startPageIndex < 1 ? 1 : startPageIndex;

            Items.Clear();

            if (startPageIndex > 1)
            {
                AddItem("1");
            }

            if (currentPage >= ((pagerCount - 1) / 2 + 2) && pageCount > 6)
            {
                AddItem("...", PagintionButtonType.Prev);
                startPageIndex++;
            }

            for (int index = startPageIndex; index < endPageIndex; index++)
            {
                AddItem(index.ToString());
            }

            if (currentPage < (pageCount - (pagerCount - 1) / 2) && pageCount > 6)
            {
                AddItem("...", PagintionButtonType.Next);
            }

            if (endPageIndex <= pageCount)
            {
                AddItem(pageCount.ToString());
            }
            _prevButton?.SetCurrentValue(IsEnabledProperty, currentPage != 1);
            _nextButton?.SetCurrentValue(IsEnabledProperty, currentPage != pageCount);
            SetCurrentValue(PageCountProperty, pageCount);
        }

        private void AddItem(string content, PagintionButtonType pagintionButtonType = PagintionButtonType.Normal)
        {
            var button = new PaginationButton()
            {
                Content = content,
                PagintionButtonType = pagintionButtonType,
                IsCurrentPage = pagintionButtonType == PagintionButtonType.Normal && int.TryParse(content, out int num) && num == CurrentPage,
            };
            button.Click += Button_Click;
            Items.Add(button);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is PaginationButton button)
            {
                switch (button.PagintionButtonType)
                {
                    case PagintionButtonType.Prev:
                        {
                            SetCurrentValue(CurrentPageProperty, Math.Max(1, CurrentPage - PagerCount + 2));
                            break;
                        }
                    case PagintionButtonType.Next:
                        {
                            SetCurrentValue(CurrentPageProperty, Math.Min(PageCount, CurrentPage + PagerCount - 2));
                            break;
                        }
                    default:
                        {
                            if (int.TryParse(button.Content?.ToString(), out int num))
                            {
                                SetCurrentValue(CurrentPageProperty, num);
                            }
                            break;
                        }
                }
            }

        }

        private static void InitPagination(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Pagination pagination)
            {
                if (e.Property == CurrentPageProperty)
                {
                    var pageChanged = new RoutedPropertyChangedEventArgs<int>((int)e.OldValue, (int)e.NewValue, PageChangedEvent);
                    pagination.RaiseEvent(pageChanged);
                    if (pageChanged.Handled)
                    {
                        return;
                    }
                }
                pagination.InitPager();
            }
        }
    }
}
