using MediatR;
using Standard.Tool.Platform.Data.Entities;
using Standard.Tool.Platform.Data.Import;
using Standard.Tool.Platform.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Auth.PermissionFeature;

public record DeletePermissionCommand(Guid Id) : IRequest<ImportResult>;

public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand, ImportResult>
{
    private readonly IRepository<PermissionEntity> _repo;

    private readonly IRepository<AccountPermissionEntity> _repoAccountPermission;

    public DeletePermissionCommandHandler(IRepository<PermissionEntity> repo) => _repo = repo;

    public async Task<ImportResult> Handle(DeletePermissionCommand request, CancellationToken ct)
    {
        var insertResult = new ImportResult();

        var exists = await _repo.AnyAsync(c => c.Id == request.Id, ct);
        if (!exists)
        {
            insertResult.Succeeded = false;
            insertResult.Message = $"Permission {request.Id} is not found.";
            return await Task.FromResult(insertResult);
        }


        var pcs = await _repoAccountPermission.GetAsync(pc => pc.PermissionId == request.Id);
        if (pcs is not null) await _repoAccountPermission.DeleteAsync(pcs, ct);

        await _repo.DeleteAsync(request.Id, ct);


        insertResult.Message = insertResult.Succeeded ? string.Empty : "";
        return await Task.FromResult(insertResult);
    }


}

