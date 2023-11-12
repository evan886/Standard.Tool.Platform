using MediatR;
using Standard.Tool.Platform.Data.Exporting;
using Standard.Tool.Platform.Data.Infrastructure;
using Standard.Tool.Platform.Materials;
using Standard.Tool.Platform.MVVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Standard.Tool.Platform.Common.Extension;
using MaterialCore = Standard.Tool.Platform.Materials;
using System.Threading.Tasks;
using Standard.Tool.Platform.Auth.AccountFeature;
using Standard.Tool.Platform.Library;
using Standard.Tool.Platform.Library.Enums;
using Standard.Tool.Platform.UserControls.Toast;
using System.Windows;
using Standard.Tool.Platform.Data.Import;
using System.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using Standard.Tool.Platform.Auth.PermissionFeature;
using Standard.Tool.Platform.Pages.Account;

namespace Standard.Tool.Platform.Pages.Project.Application.Material
{
    public class MaterialDataOperatePageViewModel : ObservableObject
    {
        private readonly IMediator _mediator;
        private readonly IReadTableService _readTableService;

        public MaterialDataOperatePageViewModel(IMediator mediator, IReadTableService readTableService)
        {
            _mediator = mediator;
            _readTableService = readTableService;
        }

        #region Property

        #region MaterialDataTable
        private ObservableCollection<MaterialCore.Material> _materialDataTable;

        public ObservableCollection<MaterialCore.Material> MaterialDataTable
        {
            get { return _materialDataTable; }
            set
            {
                _materialDataTable = value;
                RaisePropertyChanged(nameof(MaterialDataTable));
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

        #region ItemCode
        private string _itemCode;
        public string ItemCode
        {
            get { return _itemCode; }
            set
            {
                _itemCode = value;
                RaisePropertyChanged(nameof(ItemCode));
            }
        }
        #endregion

        #region ItemName
        private string _itemName;
        public string ItemName
        {
            get { return _itemName; }
            set
            {
                _itemName = value;
                RaisePropertyChanged(nameof(ItemName));
            }
        }
        #endregion

        #endregion


        #region Method

        #region 01，Export
        void ExportExecute()
        {
            Task.Run(async
            () =>
            {
                //var exportResult = await _mediator.Send(new ExportMaterialDataCommand());
            });
        }

        bool CanExportExecute()
        {
            return true;
        }

        public ICommand Export
        {
            get { return new RelayCommand(ExportExecute, CanExportExecute); }
        }
        #endregion 

        #region 02，Import
        void ImportExecute()
        {
            var materiallist = _readTableService.ReadExcel().DataSetToList<MaterialCore.Material>();
            MaterialDataTable = new ObservableCollection<MaterialCore.Material>(materiallist);

            if (materiallist.Any())
            {
                ToastControlNotification.Show("加载完毕", new ToastOptions { Icon = EnumToastType.Information, Location = EnumToastLocation.OwnerCenter, Time = 2000 });
            }   
        }

        bool CanImportExecute()
        {
            return true;
        }

        public ICommand Import
        {
            get { return new RelayCommand(ImportExecute, CanImportExecute); }
        }
        #endregion 

        #region 03，Save
        void SaveExecute()
        {
            var request = new EditMaterialRequest(MaterialDataTable);
            if (request.IsValid())
            {
                Task.Run(async () =>
                {
                    var exportResult = await _mediator.Send(new CreateMaterialDataCommand(request));

                    if (exportResult.Succeeded)
                    {
                        ToastControlNotification.Show("保存成功", new ToastOptions { Icon = EnumToastType.Information, Location = EnumToastLocation.OwnerCenter, Time = 2000 });
                    }
                    else {
                        ToastControlNotification.Show("保存失败", new ToastOptions { Icon = EnumToastType.Information, Location = EnumToastLocation.OwnerCenter, Time = 2000 });
                    }

                });
            }
        }

        bool CanSaveExecute()
        {
            return true;
        }

        public ICommand Save
        {
            get { return new RelayCommand(SaveExecute, CanSaveExecute); }
        }
        #endregion 

        #region 04，ImportData
        void ImportDataExecute()
        {
            var request = new EditMaterialRequest(MaterialDataTable);
            if (request.IsValid())
            {
                Task.Run(async () =>
                {
                    var exportResult = await _mediator.Send(new ImportMaterialDataCommand(request));

                    if (exportResult.Succeeded)
                    {
                        ToastControlNotification.Show("保存成功", new ToastOptions { Icon = EnumToastType.Information, Location = EnumToastLocation.OwnerCenter, Time = 2000 });
                    }
                    else
                    {
                        ToastControlNotification.Show("保存失败", new ToastOptions { Icon = EnumToastType.Information, Location = EnumToastLocation.OwnerCenter, Time = 2000 });
                    }

                });
            }
        }

        bool CanImportDataExecute()
        {
            return true;
        }

        public ICommand ImportData
        {
            get { return new RelayCommand(ImportDataExecute, CanImportDataExecute); }
        }
        #endregion 

        #region 05，Search
        void SearchExecute()
        {
            IEnumerable<MaterialCore.Material> dataList = null;
            Task.Run(async
                () =>
            {
                if (MaterialDataTable.Any())
                {

                    TotalCount = MaterialDataTable.Where(p =>
                    (null == ItemCode || p.DESIGNLONGUE.Contains(ItemCode))
                    && (null == ItemName || p.DESIGNLONGUE.Contains(ItemName))).Count();

                    dataList= MaterialDataTable.Where(p => 
                    (null == ItemCode || p.DESIGNLONGUE.Contains(ItemCode))
                    && (null == ItemName || p.DESIGNLONGUE.Contains(ItemName)))
                    .Skip((PageIndex-1)* PageSize).Take(PageSize);

                    MaterialDataTable = new ObservableCollection<MaterialCore.Material>(dataList);
                }
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
