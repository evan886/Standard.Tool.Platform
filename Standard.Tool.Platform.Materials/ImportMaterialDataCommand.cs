using MediatR;
using Standard.Tool.Platform.Common.Helper;
using Standard.Tool.Platform.Data.Entities.Project;
using Standard.Tool.Platform.Data.Import;
using Standard.Tool.Platform.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Materials
{
    public record ImportMaterialDataCommand(EditMaterialRequest Payload) : IRequest<ImportResult>;

    public class ImportDataCommandHandler : IRequestHandler<ImportMaterialDataCommand, ImportResult>
    {
        private readonly IOracleRepository<MaterialEntity> _repo;

        private readonly IOracleRepository<Demo> _repoDemo;

        //public ImportDataCommandHandler(IOracleRepository<MaterialEntity> repo) => _repo = repo;

        public ImportDataCommandHandler(IOracleRepository<MaterialEntity> repo,
            IOracleRepository<Demo> repoDemo) {
            _repo = repo;
            _repoDemo = repoDemo;
        } 

        public async Task<ImportResult> Handle(ImportMaterialDataCommand request, CancellationToken ct)
        {
            var insertResult = new ImportResult();
            if (request.Payload.MaterialDataList.Any())
            {
                var dtData = TransHelper<Material, MaterialEntity>.TransAutoCopyList(request.Payload.MaterialDataList.ToList());

                insertResult.Succeeded = await  _repo.Add(dtData.ToList()) > 0 ? true : false;
                insertResult.Message = insertResult.Succeeded ? string.Empty : "";
            }

            return await Task.FromResult(insertResult);
        }
    }
}
