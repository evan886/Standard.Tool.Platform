using MediatR;
using Standard.Tool.Platform.Data.Entities;
using Standard.Tool.Platform.Common.Helper;
using Standard.Tool.Platform.Data.Infrastructure;
using Standard.Tool.Platform.Data.Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Auth.PermissionFeature
{
    public record ListPermissionsSegmentQuery : IRequest<IList<Permission>>
    {
        public ListPermissionsSegmentQuery(Guid? parentId = null, PermissionType permissionType = PermissionType.Page)
        {
            ParentId = parentId;
            Type = permissionType;
        }

        public Guid? ParentId { get; set; }

        public PermissionType Type { get; set; }

    }

    public class ListPermissionsSegmentQueryHandler : IRequestHandler<ListPermissionsSegmentQuery, IList<Permission>>
    {
        private readonly IRepository<PermissionEntity> _repo;

        public ListPermissionsSegmentQueryHandler(IRepository<PermissionEntity> repo) => _repo = repo;

        public async Task<IList<Permission>> Handle(ListPermissionsSegmentQuery request, CancellationToken ct)
        {
            var spec = new PermissionSpec(request.ParentId, request.Type);

            return await _repo.SelectAsync(spec, p => new Permission
            {
                Id = p.Id.ToString().Trim(),
                CreateTimeUtc = p.CreateTimeUtc,
                Code = p.Code,
                Name = p.Name,
                Type = p.Type,
                TypeName = p.Type.GetDescription(),
                ParentId = p.ParentId,
                Status = p.Status,
                No = 0,
                //Parent = new Permission(p.Parent),
                LastModifiedTimeUtc = p.LastModifiedTimeUtc,
                Childrens = p.Childrens.Select(sm => new Permission
                {
                    Id = sm.Id.ToString().Trim(),
                    Code = sm.Code,
                    Name = sm.Name,
                    Type = sm.Type,
                    TypeName = sm.Type.GetDescription(),
                    ParentId = sm.ParentId,
                    Status = sm.Status,
                    CreateTimeUtc = sm.CreateTimeUtc,
                    LastModifiedTimeUtc = sm.LastModifiedTimeUtc,
                }).ToArray()
            });
        }
    }
}
