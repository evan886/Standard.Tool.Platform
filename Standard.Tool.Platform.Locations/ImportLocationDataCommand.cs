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

namespace Standard.Tool.Platform.Locations
{
    public record ImportLocationDataCommand(EditLocationRequest Payload) : IRequest<ImportResult>;

    public class ImportDataCommandHandler : IRequestHandler<ImportLocationDataCommand, ImportResult>
    {
        private readonly IOracleRepository<LocationEntity> _repo;

        public ImportDataCommandHandler(IOracleRepository<LocationEntity> repo)=>_repo = repo;

        public async Task<ImportResult> Handle(ImportLocationDataCommand request, CancellationToken ct)
        {
            var insertResult = new ImportResult();
            if (request.Payload.LocationDataList.Any())
            {
                var dtData = TransHelper<Location, LocationEntity>.TransAutoCopyList(request.Payload.LocationDataList.ToList());

                insertResult.Succeeded = await _repo.Add(dtData.ToList()) > 0 ? true : false;
                insertResult.Message = insertResult.Succeeded ? string.Empty : "";
            }

            return await Task.FromResult(insertResult);
        }
    }
}
