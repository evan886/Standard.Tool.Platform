using Standard.Tool.Platform.Data.Entities;
using Standard.Tool.Platform.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Standard.Tool.Platform.Auth.PermissionFeature;

public enum PermissionsSortBy
{
    Recent,
    Featured
}

public sealed class PermissionPagingSpec : BaseSpecification<PermissionEntity>
{
    public PermissionPagingSpec(int pageSize, int pageIndex, 
        string? permissionCode = null,string? permissionName=null,
        string? status= null,
        PermissionsSortBy postsSortBy = PermissionsSortBy.Recent)
        : base(p => (null == permissionCode || p.Code.Contains(permissionCode))
        && (null == permissionName || p.Name.Contains(permissionName))
        && (null == status || p.Status== status))
    {
        var startRow = (pageIndex - 1) * pageSize;

        switch (postsSortBy)
        {
            case PermissionsSortBy.Recent:
                ApplyOrderByDescending(p => p.CreateTimeUtc);
                break;
            case PermissionsSortBy.Featured:
                ApplyOrderByDescending(p => p.Type);
                break;
        }
        ApplyPaging(startRow, pageSize);
    }
}

