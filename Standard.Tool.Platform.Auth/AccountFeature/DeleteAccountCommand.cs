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

namespace Standard.Tool.Platform.Auth.AccountFeature;

public record DeleteAccountCommand(Guid Id) : IRequest<ImportResult>;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, ImportResult>
{
    private readonly IRepository<AccountEntity> _repo;

    public DeleteAccountCommandHandler(IRepository<AccountEntity> repo) => _repo = repo;

    public async Task<ImportResult> Handle(DeleteAccountCommand request, CancellationToken ct)
    {
        var insertResult = new ImportResult();

        var account = await _repo.GetAsync(request.Id, ct);
        if (account != null) insertResult.Succeeded = await _repo.DeleteAsync(request.Id, ct) ==1;

        insertResult.Message = insertResult.Succeeded ? string.Empty : "";
        return await Task.FromResult(insertResult);
    }


}
