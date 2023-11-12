using MediatR;
using Standard.Tool.Platform.Data.Entities;
using Standard.Tool.Platform.Data.Infrastructure;
using Standard.Tool.Platform.Data.Spec;
using System.Threading;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Auth.PermissionFeature;

public record CountPermissionsQuery(string code,string name,string status) : IRequest<int>;

public class CountPermissionsQueryHandler : IRequestHandler<CountPermissionsQuery, int>
{
    private readonly IRepository<PermissionEntity> _repo;

    public CountPermissionsQueryHandler(IRepository<PermissionEntity> repo) => _repo = repo;

    public async Task<int> Handle(CountPermissionsQuery request, CancellationToken ct)
    {
        int count = 0;
        var spec = new PermissionSpec(request.code, request.name, request.status);
        count= await _repo.CountAsync(spec);
        return count;
    }
}
