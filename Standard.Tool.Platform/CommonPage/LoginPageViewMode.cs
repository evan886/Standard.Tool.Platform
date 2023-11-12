using Microsoft.Extensions.DependencyInjection;
using Standard.Tool.Platform.Extension;
using Standard.Tool.Platform.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Standard.Tool.Platform.CommonPage
{
    public class LoginPageViewMode:ObservableObject
    {

        #region 属性

        #region 用户名
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged("UserName");
            }
        }
        #endregion

        #region 密码
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }
        #endregion 

        #region 激活码
        private string _key;
        public string Key
        {
            get { return _key; }
            set
            {
                _key = value;
                RaisePropertyChanged("Key");
            }
        }
        #endregion 

        #endregion

        public LoginPageViewMode()
        {
            
        }


        #region 方法

        #region 01，登录操作
        public void SignInExecute()
        {
            if ("admin".Equals(UserName) && "1111".Equals(Password))
            {
                ProviderFactory.ServiceProvider?.GetService<MainWindow>()?.Show();
                ProviderFactory.ServiceProvider?.GetService<LoginPage>()?.Close();
            }
            else
            {
                MessageBox.Show("用户名或密码错误");
                UserName = string.Empty;
                Password=string.Empty;
            }
        }

        public bool CanSignInExecute()
        {
            return !string.IsNullOrWhiteSpace(UserName)
                &&!string.IsNullOrWhiteSpace(Password);
        }

        public ICommand SignIn
        {
            get { return new RelayCommand(SignInExecute, CanSignInExecute); }
        }
        #endregion 

        #region 02，激活操作
        public void ActivateExecute()
        {
            if ("00000-11111-22222-33333-44444-55555".Equals(Key))
            {
                MessageBox.Show("激活成功");
                ProviderFactory.ServiceProvider?.GetService<MainWindow>()?.Show();
                ProviderFactory.ServiceProvider?.GetService<LoginPage>()?.Close();
            }
            
        }

        public bool CanActivateExecute()
        {
            return true;
        }

        public ICommand Activate
        {
            get { return new RelayCommand(ActivateExecute, CanActivateExecute); }
        }
        #endregion 

        #region 03，关闭操作
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
        #endregion
    }
}
