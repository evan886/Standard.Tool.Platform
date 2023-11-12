using MediatR;
using Standard.Tool.Platform.Data.Entities.Project;
using Standard.Tool.Platform.Data.Exporting.Exporters;
using Standard.Tool.Platform.Data.Import;
using Standard.Tool.Platform.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Data.Exporting
{
    public record ExportMaterialDataCommand : IRequest<ExportResult>;

    public class ExportMaterialDataCommandHandler : IRequestHandler<ExportMaterialDataCommand, ExportResult>
    {
        private readonly IRepository<MaterialEntity> _repo;
        public ExportMaterialDataCommandHandler(IRepository<MaterialEntity> repo) => _repo = repo;

        public Task<ExportResult> Handle(ExportMaterialDataCommand request, CancellationToken cancellationToken)
        {
            var catExp = new CSVExporter<MaterialEntity>(_repo, "standard-tool-platform-aterials", ExportManager.DataDir);
            return catExp.ExportData(p => new MaterialEntity(), cancellationToken);
        }
    }
}
