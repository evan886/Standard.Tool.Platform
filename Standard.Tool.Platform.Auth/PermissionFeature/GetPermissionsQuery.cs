using MediatR;
using NPOI.POIFS.Properties;
using Org.BouncyCastle.Asn1.X509;
using Standard.Tool.Platform.Common.Helper;
using Standard.Tool.Platform.Data.Entities;
using Standard.Tool.Platform.Data.Infrastructure;
using Standard.Tool.Platform.Data.Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Auth.PermissionFeature;

public record GetPermissionsQuery : IRequest<IList<Permission>>
{
    public int PageSize { get; set; }
    public int PageIndex { get; set; }

    public string PermissionCode { get; set; }

    public string PermissionName { get; set; }

    public string Status { get; set; }

    public GetPermissionsQuery(int pageIndex, int pageSize,string permissionCode,string permissionName,string status)
    {
        PageSize = pageSize;
        PageIndex = pageIndex;
        PermissionCode= permissionCode;
        PermissionName= permissionName;
        Status= status;
    }
}

public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, IList<Permission>>
{
    private readonly IRepository<PermissionEntity> _repo;

    public GetPermissionsQueryHandler(IRepository<PermissionEntity> repo) => _repo = repo;

    public async Task<IList<Permission>> Handle(GetPermissionsQuery request, CancellationToken ct)
    {
        if (request.PageSize < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(request.PageSize),
                $"{nameof(request.PageSize)} can not be less than 1, current value: {request.PageSize}.");
        }

        if (request.PageIndex < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(request.PageIndex),
                $"{nameof(request.PageIndex)} can not be less than 1, current value: {request.PageIndex}.");
        }

        var spec = new PermissionPagingSpec(request.PageSize, request.PageIndex,request.PermissionCode,request.PermissionName,request.Status);
        return await _repo.SelectAsync(spec, Permission.EntitySelector);

        #region 暂时注释
        //var data = await _repo.SelectAsync((p) => new Permission
        //{
        //    Id = p.Id.ToString().Trim(),
        //    //IsShow = true,
        //    CreateTimeUtc = p.CreateTimeUtc,
        //    Code = p.Code,
        //    Name = p.Name,
        //    Type = p.Type,
        //    TypeName = p.Type.GetDescription(),
        //    ParentId = p.ParentId,
        //    Status = p.Status,
        //    No = 0,
        //    Parent = new Permission(p.Parent),
        //    LastModifiedTimeUtc = p.LastModifiedTimeUtc,
        //    Childrens = p.Childrens.Select(sm => new Permission
        //    {
        //        Id = sm.Id.ToString().Trim(),
        //        Code = sm.Code,
        //        Name = sm.Name,
        //        Type = sm.Type,
        //        TypeName = sm.Type.GetDescription(),
        //        ParentId = sm.ParentId,
        //        Status = sm.Status,
        //        CreateTimeUtc = sm.CreateTimeUtc,
        //        LastModifiedTimeUtc = sm.LastModifiedTimeUtc,
        //    }).ToArray()
        //}, ct); 
        //return data; 
        #endregion
    }
}
