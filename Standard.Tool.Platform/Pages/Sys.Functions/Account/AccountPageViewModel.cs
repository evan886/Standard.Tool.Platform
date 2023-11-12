using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Standard.Tool.Platform.Auth;
using Standard.Tool.Platform.Auth.AccountFeature;
using Standard.Tool.Platform.Extension;
using Standard.Tool.Platform.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AuthCore = Standard.Tool.Platform.Auth;

namespace Standard.Tool.Platform.Pages.Account;

public class AccountPageViewModel : ObservableObject
{
    private readonly IMediator _mediator;
    public AccountPageViewModel(IMediator mediator)
    {
        _mediator = mediator;
        RefreshExecute();
        SearchExecute();
    }

    #region Property

    #region UserName
    private string _userName;
    public string UserName
    {
        get { return _userName; }
        set
        {
            _userName = value;
            RaisePropertyChanged(nameof(UserName));
        }
    }
    #endregion

    #region LoginName
    private string _loginName;
    public string LoginName
    {
        get { return _loginName; }
        set
        {
            _loginName = value;
            RaisePropertyChanged(nameof(LoginName));
        }
    }
    #endregion

    #region Status
    private StatusValue _status;
    public StatusValue Status
    {
        get { return _status; }
        set
        {
            _status = value;
            RaisePropertyChanged(nameof(Status));
        }
    }

    private ObservableCollection<StatusValue> _statusList;
    public ObservableCollection<StatusValue> StatusList
    {
        get { return _statusList; }
        set
        {
            _statusList = value;
            RaisePropertyChanged(nameof(StatusList));
        }
    }
    #endregion

    #region UserList
    private ObservableCollection<AuthCore.AccountFeature.Account> _userList;

    public ObservableCollection<AuthCore.AccountFeature.Account> UserList
    {
        get { return _userList; }
        set
        {
            _userList = value;
            RaisePropertyChanged(nameof(UserList));
        }
    }
    #endregion

    #endregion


    #region Method

    #region 01，Refresh
    void RefreshExecute()
    {
        UserName = string.Empty;
        LoginName=string.Empty;

        var statusData = new List<StatusValue>();
        statusData.Add(new StatusValue() { Name = "==请选择==", Value = "" });
        statusData.Add(new StatusValue() { Name = "启用", Value = "Enable" });
        statusData.Add(new StatusValue() { Name = "禁用", Value = "Disable" });

        StatusList = new ObservableCollection<StatusValue>(statusData);

        Status = StatusList?.FirstOrDefault();
    }

    bool CanRefreshExecute()
    {
        return true;
    }

    public ICommand Refresh
    {
        get { return new RelayCommand(RefreshExecute, CanRefreshExecute); }
    }
    #endregion

    #region 02，Search
    void SearchExecute()
    {
        IList<AuthCore.AccountFeature.Account> dataList = null;
        Task.Run(async
            () =>
        {
            dataList = await _mediator.Send(new GetAccountsQuery());

            if (dataList != null)
                UserList = new ObservableCollection<AuthCore.AccountFeature.Account>(dataList);
        });
    }

    bool CanSearchExecute()
    {
        return true;
    }

    public ICommand Search
    {
        get { return new RelayCommand(SearchExecute, CanSearchExecute); }
    }
    #endregion

    #region 03，AssignUserPermissions
    void AssignUserPermissionsExecute(string userId)
    {
        ProviderFactory.ServiceProvider?
            .GetRequiredService<AssignUserPermissionPage>()?
            .Init(userId).Show();
    }

    bool CanAssignUserPermissionsExecute(string userId)
    {
        return true;
    }

    public ICommand AssignUserPermissions
    {
        get { return new RelayCommand<string>(AssignUserPermissionsExecute, CanAssignUserPermissionsExecute); }
    }
    #endregion




    #endregion
}
