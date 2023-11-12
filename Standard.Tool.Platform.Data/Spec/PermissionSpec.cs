using ICSharpCode.SharpZipLib.Zip;
using Microsoft.EntityFrameworkCore;
using NPOI.XWPF.UserModel;
using SixLabors.Fonts.Tables.AdvancedTypographic;
using Standard.Tool.Platform.Auth.PermissionFeature;
using Standard.Tool.Platform.Data.Entities;
using Standard.Tool.Platform.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Data.Spec;

public sealed class PermissionSpec : BaseSpecification<PermissionEntity>
{
    public PermissionSpec(Guid? parentId, PermissionType permissiontype = PermissionType.Module)
        : base(p => p.ParentId== parentId && p.Type== permissiontype)
    {
        AddInclude(permission => permission
            //.Include(c => c.Parent)
            .Include(c => c.Childrens)
            .Include(c=>c.AccountPermissions));  
    }

    public PermissionSpec(string? permissionCode = null, string? permissionName = null,
       string? status = null,
       PermissionsSortBy postsSortBy = PermissionsSortBy.Recent)
       : base(p => (null == permissionCode || p.Code.Contains(permissionCode))
       && (null == permissionName || p.Name.Contains(permissionName))
       && (null == status || p.Status == status))
    {

        switch (postsSortBy)
        {
            case PermissionsSortBy.Recent:
                ApplyOrderByDescending(p => p.CreateTimeUtc);
                break;
            case PermissionsSortBy.Featured:
                ApplyOrderByDescending(p => p.Type);
                break;
        }
    }
}
