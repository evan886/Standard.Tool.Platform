using MediatR;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Standard.Tool.Platform.Pages.Account;

/// <summary>
/// AssignUserPermissionPage.xaml 的交互逻辑
/// </summary>
public partial class AssignUserPermissionPage : Window
{
    AssignUserPermissionPageViewModel _assignUserPermissionPageViewModel;
    public AssignUserPermissionPage(IMediator mediator)
    {
        InitializeComponent();
        _assignUserPermissionPageViewModel= new AssignUserPermissionPageViewModel(mediator);
        DataContext = _assignUserPermissionPageViewModel;
    }

    public AssignUserPermissionPage Init(string userId)
    {
        _assignUserPermissionPageViewModel.QueryAccountById(userId);
        return this;
    }

    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        this.DragMove();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        e.Cancel = true;
        this.Hide();
    }
}
