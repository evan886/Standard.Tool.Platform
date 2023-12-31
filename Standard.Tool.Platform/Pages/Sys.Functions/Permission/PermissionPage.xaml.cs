﻿using MediatR;
using Standard.Tool.Platform.Pages.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Standard.Tool.Platform.Pages.Account
{
    /// <summary>
    /// PermissionPage.xaml 的交互逻辑
    /// </summary>
    public partial class PermissionPage : Page
    {
        public PermissionPage(IMediator mediator)
        {
            InitializeComponent();
            DataContext = new PermissionPageViewModel(mediator);

        }
    }
}
