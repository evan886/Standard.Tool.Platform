using MediatR;
using Standard.Tool.Platform.Data.Infrastructure;
using System.Windows.Controls;

namespace Standard.Tool.Platform.Pages.Project.Application.Material;

/// <summary>
/// MaterialDataOperatePage.xaml 的交互逻辑
/// </summary>
public partial class MaterialDataOperatePage : Page
{
    public MaterialDataOperatePage(IMediator mediator, IReadTableService readTableService)
    {
        InitializeComponent();
        DataContext = new MaterialDataOperatePageViewModel(mediator, readTableService);
    }
}
