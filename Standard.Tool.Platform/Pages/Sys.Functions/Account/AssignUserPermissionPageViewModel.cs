using MediatR;
using Standard.Tool.Platform.Auth.AccountFeature;
using Standard.Tool.Platform.CommonPage;
using Standard.Tool.Platform.Extension;
using Standard.Tool.Platform.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using AuthCore = Standard.Tool.Platform.Auth;
using Standard.Tool.Platform.Auth.PermissionFeature;
using Standard.Tool.Platform.Data.Entities;
using NPOI.SS.Formula.Functions;
using Standard.Tool.Platform.Materials;
using NPOI.HSSF.Record.PivotTable;
using Microsoft.VisualBasic;

namespace Standard.Tool.Platform.Pages.Account
{
    public class AssignUserPermissionPageViewModel : ObservableObject
    {
        private readonly IMediator _mediator;
        public AssignUserPermissionPageViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }


        #region Property

        #region ModuleList
        private ObservableCollection<AuthCore.PermissionFeature.Permission> _moduleList;

        public ObservableCollection<AuthCore.PermissionFeature.Permission> ModuleList
        {
            get { return _moduleList; }
            set
            {
                _moduleList = value;
                RaisePropertyChanged(nameof(ModuleList));
            }
        }
        #endregion

        #region PermissionList
        private ObservableCollection<AuthCore.PermissionFeature.Permission> _permissionList;

        public ObservableCollection<AuthCore.PermissionFeature.Permission> PermissionList
        {
            get { return _permissionList; }
            set
            {
                _permissionList = value;
                RaisePropertyChanged(nameof(PermissionList));
            }
        }
        #endregion

        #region ShowPermissionList
        private ObservableCollection<AuthCore.PermissionFeature.Permission> _showPermissionList;

        public ObservableCollection<AuthCore.PermissionFeature.Permission> ShowPermissionList
        {
            get { return _showPermissionList; }
            set
            {
                _showPermissionList = value;
                RaisePropertyChanged(nameof(ShowPermissionList));
            }
        }
        #endregion

        #region AllPermissionList
        private ObservableCollection<AuthCore.PermissionFeature.Permission> _allPermissionList;

        public ObservableCollection<AuthCore.PermissionFeature.Permission> AllPermissionList
        {
            get { return _allPermissionList; }
            set
            {
                _allPermissionList = value;
                RaisePropertyChanged(nameof(AllPermissionList));
            }
        }
        #endregion

        #region Account
        public AuthCore.AccountFeature.Account Account { get; set; }
        #endregion

        #endregion

        #region 方法

        #region 01，SelectModule
        async void SelectModuleExecute(string permissionId)
        {
            await Task.Run(async
                 () =>
             {
                 await Refresh();

                 ShowPermissionList = new ObservableCollection<AuthCore.PermissionFeature.Permission>(PermissionList.Where(u => u.ParentId == Guid.Parse(permissionId)));


                 AllPermissionList.ToList().ForEach(a =>
                 {
                     //Page Levels
                     PermissionList.ToList().ForEach(b =>
                     {
                         if (b.Id == a.Id)
                             a.IsSelected = b.IsSelected;
                     });
                 });

             });
        }


        bool CanSelectModuleExecute(string permissionId)
        {
            return true;
        }

        public ICommand SelectModule
        {
            get { return new RelayCommand<string>(SelectModuleExecute, CanSelectModuleExecute); }
        }

        async Task Refresh()
        {
            await Task.Run(async
                 () =>
            {

                if (ShowPermissionList == null) return;
                ShowPermissionList.ToList().ForEach(a =>
                {
                    //Page Levels
                    PermissionList.ToList().ForEach(b =>
                    {
                        if (b.Id == a.Id)
                            b.IsSelected = a.IsSelected;
                    });

                    if (!a.IsSelected && AllPermissionList.Any(u => u.Id == a.Id))
                    {
                        AllPermissionList.RemoveAt(Array.IndexOf(AllPermissionList.ToArray(), AllPermissionList.ToList().Find(p => p.Id == a.Id)));
                    }
                    else if (a.IsSelected)
                    {
                        if (AllPermissionList.Any(u => u.Id == a.Id))
                        {
                            AllPermissionList.ToList().ForEach(c =>
                            {
                                if (c.Id == a.Id)
                                    c.IsSelected = a.IsSelected;
                            });
                        }
                        else
                            AllPermissionList.Add(a);

                    }


                    //Control Levels
                    if (a.Childrens is { Length: > 0 })
                    {
                        a.Childrens.ToList().ForEach(c =>
                        {
                            PermissionList.ToList().ForEach(d =>
                            {
                                if (d.Childrens is { Length: > 0 })
                                {
                                    d.Childrens.ToList().ForEach(e =>
                                    {
                                        if (c.Id == e.Id)
                                            e.IsSelected = c.IsSelected;

                                        if (!e.IsSelected && AllPermissionList.Any(u => u.Id == e.Id))
                                        {
                                            AllPermissionList.RemoveAt(Array.IndexOf(AllPermissionList.ToArray(), AllPermissionList.ToList().Find(p => p.Id == e.Id)));
                                        }
                                        else if (e.IsSelected)
                                        {
                                            if (AllPermissionList.Any(u => u.Id == e.Id))
                                            {
                                                AllPermissionList.ToList().ForEach(w =>
                                                {
                                                    if (w.Id == e.Id)
                                                        w.IsSelected = e.IsSelected;
                                                });
                                            }
                                            else
                                                AllPermissionList.Add(e);
                                        }
                                    });
                                }
                            });
                        });
                    }
                });
            });
        }
        #endregion

        #region 02，Search
        async void SearchExecute()
        {
            IList<AuthCore.PermissionFeature.Permission> dataList = null;
            await Task.Run(async
                 () =>
             {
                 dataList = await _mediator.Send(new ListPermissionsQuery());

                 if (dataList == null) return;
                 ModuleList = new ObservableCollection<AuthCore.PermissionFeature.Permission>(dataList.Where(u => u.ParentId == null));
                 PermissionList = new ObservableCollection<AuthCore.PermissionFeature.Permission>(dataList);
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

        #region 03，Closed
        public void CloseExecute()
        {
            ProviderFactory.ServiceProvider?.GetRequiredService<AssignUserPermissionPage>().Close();
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

        #region 04，AccountPermission
        public async void QueryAccountById(string userId)
        {
            await Task.Run(async
                 () =>
             {
                 Account = await _mediator.Send(new GetAccountByIdQuery(Guid.Parse(userId)));
                 var permissionList = await _mediator.Send(new ListPermissionsQuery());

                 AllPermissionList = new ObservableCollection<AuthCore.PermissionFeature.Permission>(permissionList);
                 ModuleList = new ObservableCollection<AuthCore.PermissionFeature.Permission>(permissionList.Where(u => u.ParentId == null));
                 ShowPermissionList = new ObservableCollection<AuthCore.PermissionFeature.Permission>(permissionList.Where(u => u.ParentId != null && u.Type == PermissionType.Page));

                 //Judge Page CheckBox and Control CheckBox Whether IsChecked
                 if (ShowPermissionList == null) return;
                 if (Account.Permissions is { Length: > 0 })
                 {
                     foreach (var permission in Account.Permissions)
                     {
                         //Page Levels
                         if (ShowPermissionList.Any(u => u.Id == permission.Id))
                             ShowPermissionList.ToList().ForEach(a => {
                                 if (a.Id == permission.Id)
                                     a.IsSelected = true;
                             });

                         //Control Levels
                         ShowPermissionList.ToList().ForEach(a =>
                         {
                             if (a.Childrens is { Length: > 0 })
                             {
                                 if (a.Childrens.Any(u => u.Id == permission.Id))
                                     a.Childrens.ToList().ForEach(b => { 
                                         if(b.Id==permission.Id)
                                            b.IsSelected = true; 
                                     });
                             }
                         });
                     }
                 }

                 PermissionList = new ObservableCollection<AuthCore.PermissionFeature.Permission>(ShowPermissionList);
             });
        }
        #endregion

        #region 05，Save
        public async void SaveExecute()
        {
            // to do
           await Task.Run(async () =>
            {
                await Refresh();
                var ids =
                    AllPermissionList.Where(u => u.ParentId != null && u.IsSelected).Select(u => Guid.Parse(u.Id)).ToArray();
                var request = new EditAccountPermissionRequest(ids);
                if (request.IsValid())
                {
                    var exportResult = await _mediator.Send(new UpdateAccountCommand(Guid.Parse(Account.Id), request));

                }
            });
        }

        public bool CanSaveExecute()
        {
            return true;
        }

        public ICommand Save
        {
            get { return new RelayCommand(SaveExecute, CanSaveExecute); }
        }
        #endregion

        #endregion


    }
}
