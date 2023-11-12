using MaterialDesignThemes.Wpf;
using Standard.Tool.Platform.MVVM;
using Standard.Tool.Platform.Common;
using Standard.Tool.Platform.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Standard.Tool.Platform.MainWindow;
using System.Text.Json.Serialization;
using Standard.Tool.Platform.Common.Helper;
using System.Reflection;
using System.Windows.Documents;

namespace Standard.Tool.Platform
{
    public class MainWindowViewModel: ObservableObject
    {
        public MainWindowViewModel() 
        {
        }

        public MainWindowViewModel(MainWindow mainWindow)
        {
            var menus = JsonHelper.DeserializeJsonToObject<List<ItemMenu>>(TableDataHelper.GetData(nameof(Menu)));
            if (menus != null && menus.Count > 0)
            {
                foreach (var item in menus)
                {
                    mainWindow.Menu.Children.Add(new UserControlMenuItem(item, mainWindow));
                }
            }
        }

        #region Property

        #region Page Name
        private string _pageName;
        public string PageName
        {
            get { return _pageName; }
            set
            {
                _pageName = value;
                RaisePropertyChanged("PageName");
            }
        }
        #endregion

        #region Page Version
        private string _pageVersion;
        public string PageVersion
        {
            get { return _pageVersion; }
            set
            {
                _pageVersion = value;
                RaisePropertyChanged("PageVersion");
            }
        }
        #endregion

        #region Current Page
        private Page _currentPage;
        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                if (value != null)
                {
                    PageName = value.Title;
                    string strVer = "";
                    foreach (Attribute attr in value.GetType().GetCustomAttributes(true))
                    {
                        if (attr is UIModuleMatadataAttribute)
                        {
                            if (attr != null)
                            {
                                strVer = (attr as UIModuleMatadataAttribute).Version;
                                break;
                            }
                        }
                    }
                    PageVersion = strVer;
                }
                else
                {
                    PageName = "";
                    PageVersion = "";
                }
                RaisePropertyChanged("CurrentPage");
            }
        }
        #endregion

        #endregion

        #region Method

        #region 01，Navigation Page
        internal void SwitchScreen(object sender)
        {
            var screen = ((Page)sender);

            if (screen != null)
            {
                CurrentPage = screen;
            }
        }
        #endregion

        #region 02，关闭操作
        public void CloseExecute()
        {
            Application.Current.Shutdown();
        }

        public bool CanCloseExecute()
        {
            return true;
        }

        public ICommand Close
        {
            get { return new RelayCommand(CloseExecute, CanCloseExecute); }
        }
        #endregion 

        #region 03，最大化操作
        public void MaxExecute(MainWindow windows)
        {
            if(windows.WindowState== WindowState.Normal) windows.WindowState = WindowState.Maximized;
            else if(windows.WindowState == WindowState.Maximized) windows.WindowState = WindowState.Normal;
        }

        public bool CanMaxExecute(MainWindow windows)
        {
            return true;
        }

        public ICommand Max
        {
            get { return new RelayCommand<MainWindow>(MaxExecute, CanMaxExecute); }
        }
        #endregion 

        #region 04，最小化操作
        public void MinExecute(MainWindow windows)
        {
            windows.WindowState = WindowState.Minimized;
        }

        public bool CanMinExecute(MainWindow windows)
        {
            return true;
        }

        public ICommand Min
        {
            get { return new RelayCommand<MainWindow>(MinExecute, CanMinExecute); }
        }
        #endregion 

        #endregion
    }

    public class ItemMenu
    {
        public ItemMenu()
        { }
        public ItemMenu(string headName, List<SubItem> subItems, PackIconKind icon)
        {
            HeadName = headName;
            SubItems = subItems;
            Icon = icon;
        }

        public string HeadName { get;  set; }
        public PackIconKind Icon { get;  set; }
        public List<SubItem> SubItems { get;  set; }
    }

    public class SubItem
    {
        public SubItem()
        {
            
        }
        public SubItem(string name, string screenName = null)
        {
            Name = name;
            ScreenName = screenName;
        }

        public string Name { get;  set; }
        public string ScreenName { get;  set; }
    }
}
