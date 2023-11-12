using MediatR;
using Standard.Tool.Platform.Data.Entities;
using Standard.Tool.Platform.Data.Infrastructure;
using Standard.Tool.Platform.Data.Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Auth.AccountFeature
{
    public record GetAccountByIdQuery(Guid Id) : IRequest<Account>;

    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, Account>
    {
        private readonly IRepository<AccountEntity> _repo;

        public GetAccountByIdQueryHandler(IRepository<AccountEntity> repo) => _repo = repo;

        public Task<Account> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new AccountSpec(request.Id);
            var account = _repo.FirstOrDefaultAsync(spec, Account.EntitySelector);
            return account;
        }
    }
}
