using MediatR;
using Standard.Tool.Platform.Data.Infrastructure;
using Standard.Tool.Platform.Library.Enums;
using Standard.Tool.Platform.Library;
using Standard.Tool.Platform.Materials;
using Standard.Tool.Platform.MVVM;
using Standard.Tool.Platform.UserControls.Toast;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LocationCore = Standard.Tool.Platform.Locations;
using Standard.Tool.Platform.Locations;
using Standard.Tool.Platform.Common.Extension;

namespace Standard.Tool.Platform.Pages.Project.Application.Location
{
    public class LocationDataOperatePageViewModel : ObservableObject
    {
        private readonly IMediator _mediator;
        private readonly IReadTableService _readTableService;

        public LocationDataOperatePageViewModel(IMediator mediator, IReadTableService readTableService)
        {
            _mediator = mediator;
            _readTableService = readTableService;
        }

        #region Property

        #region MaterialDataTable
        private ObservableCollection<LocationCore.Location> _locationDataTable;

        public ObservableCollection<LocationCore.Location> LocationDataTable
        {
            get { return _locationDataTable; }
            set
            {
                _locationDataTable = value;
                RaisePropertyChanged(nameof(LocationDataTable));
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
            var materiallist = _readTableService.ReadExcel().DataSetToList<LocationCore.Location>();
            LocationDataTable = new ObservableCollection<LocationCore.Location>(materiallist);

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
            var request = new EditLocationRequest(LocationDataTable);
            if (request.IsValid())
            {
                Task.Run(async () =>
                {
                    var exportResult = await _mediator.Send(new CreateLocationDataCommand(request));

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
            var request = new EditLocationRequest(LocationDataTable);
            if (request.IsValid())
            {
                Task.Run(async () =>
                {
                    var exportResult = await _mediator.Send(new ImportLocationDataCommand(request));

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

        #endregion

    }
}
