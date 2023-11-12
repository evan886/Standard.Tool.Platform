using MediatR;
using Standard.Tool.Platform.Auth.AccountFeature;
using Standard.Tool.Platform.Auth.PermissionFeature;
using Standard.Tool.Platform.MVVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using PermissionCore = Standard.Tool.Platform.Auth.PermissionFeature;

namespace Standard.Tool.Platform.Pages.Permission
{
    public class PermissionPageViewModel : ObservableObject
    {
        private readonly IMediator _mediator;
        public PermissionPageViewModel( IMediator mediator)
        {
            _mediator = mediator;
            PageIndex = 1;
            PageSize = 10;

            RefreshExecute();
            SearchExecute();
        }


        #region Property

        #region PermissionCode
        private string _permissionCode;
        public string PermissionCode
        {
            get { return _permissionCode; }
            set
            {
                _permissionCode = value;
                RaisePropertyChanged(nameof(PermissionCode));
            }
        }
        #endregion

        #region PermissionName
        private string _permissionName;
        public string PermissionName
        {
            get { return _permissionName; }
            set
            {
                _permissionName = value;
                RaisePropertyChanged(nameof(PermissionName));
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

        #region PermissionList
        private ObservableCollection<PermissionCore.Permission> _permissionList;

        public ObservableCollection<PermissionCore.Permission> PermissionList
        {
            get { return _permissionList; }
            set
            {
                _permissionList = value;
                RaisePropertyChanged(nameof(PermissionList));
            }
        }
        #endregion

        #region PageIndex
        private int _pageIndex;
        public int PageIndex
        {
            get { return _pageIndex; }
            set
            {
                _pageIndex = value;
                RaisePropertyChanged(nameof(PageIndex));
                SearchExecute();
            }
        }
        #endregion

        #region PageSize
        private int _pageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value;
                RaisePropertyChanged(nameof(PageSize));
            }
        }
        #endregion

        #region TotalCount
        private int _totalCount;
        public int TotalCount
        {
            get { return _totalCount; }
            set
            {
                _totalCount = value;
                RaisePropertyChanged(nameof(TotalCount));
            }
        }
        #endregion
        #endregion

        #region Method

        #region 01，Refresh
        void RefreshExecute()
        {
            PermissionName = string.Empty;
            PermissionCode = string.Empty;

            var statusData = new List<StatusValue>();
            statusData.Add(new StatusValue() { Name = "==请选择==", Value = null });
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
            IList<PermissionCore.Permission> dataList = null;
            Task.Run(async
                () =>
            {

                TotalCount = await _mediator.Send(new CountPermissionsQuery(PermissionCode, PermissionName, Status.Value));

                dataList = await _mediator.Send(new GetPermissionsQuery(PageIndex,PageSize,PermissionCode,PermissionName,Status.Value));

                if (dataList != null)
                    PermissionList = new ObservableCollection<PermissionCore.Permission>(dataList);

                
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


        #endregion
    }
}
