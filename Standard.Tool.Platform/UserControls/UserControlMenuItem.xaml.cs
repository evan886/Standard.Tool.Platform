using Standard.Tool.Platform.Extension;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Standard.Tool.Platform.UserControls
{
    /// <summary>
    /// UserControlMenuItem.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlMenuItem : UserControl
    {
        MainWindow _context;
        public UserControlMenuItem(ItemMenu itemMenu, MainWindow context)
        {
            InitializeComponent();

            _context = context;

            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;
            ListViewItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;

            this.DataContext = itemMenu;
        }

        private void ListViewMenu_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            var assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
            var types = assembly.ExportedTypes;
            var screenName = ((SubItem)((ListView)sender).SelectedItem).ScreenName;
            Type exportType = null;
            foreach (var type in assembly.ExportedTypes)
            {
                if (type.Name.Equals(screenName))
                {
                    exportType = type;
                    break;
                }
            }

            if (exportType != null)
            {
                //var screen = assembly.CreateInstance(fullName); //通过制定类完全限定名，动态获取对象实例
                var screen = ProviderFactory.ServiceProvider.GetService(exportType);//通過Service Container 获取对象 
                if (screen != null)
                {
                    _context.SwitchScreen(screen);
                }
            }
        }
    }
}
