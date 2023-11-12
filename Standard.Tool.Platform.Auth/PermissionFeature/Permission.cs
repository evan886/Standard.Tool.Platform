using Standard.Tool.Platform.Common.Helper;
using Standard.Tool.Platform.Data.Entities;
using Standard.Tool.Platform.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Standard.Tool.Platform.Auth.PermissionFeature;

public class Permission//: ObservableObject
{
    /// <summary>
    /// 序号
    /// </summary>
    public int No { get; set; }


    /// <summary>
    /// 是否选中
    /// </summary>
    public bool IsSelected { get; set; }


    /// <summary>
    /// GUID
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// 权限名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 权限编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 模块类型
    /// </summary>
    public PermissionType Type { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    public string TypeName { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// 父级Id
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 最后修改时间
    /// </summary>
    public DateTime? LastModifiedTimeUtc { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTimeUtc { get; set; }

    /// <summary>
    /// 父级
    /// </summary>
    public Permission Parent { get; set; }

    /// <summary>
    /// 子级
    /// </summary>
    public Permission[] Childrens { get; set; }

    public Permission(PermissionEntity entity)
    {
        if (entity is null) return;
        if (entity.Parent is null) return;
        {
            //IsShow= true;
            Id = entity.Parent.Id.ToString().Trim();
            Code = entity.Parent.Code;
            ParentId = entity.Parent.ParentId;
            Status = entity.Parent.Status;
            Name = entity.Parent.Name;
            Type = entity.Parent.Type;
            TypeName = entity.Parent.Type.GetDescription();
            CreateTimeUtc = entity.Parent.CreateTimeUtc;
            LastModifiedTimeUtc = entity.Parent.LastModifiedTimeUtc;
        }
    }

    public static Expression<Func<PermissionEntity, Permission>> EntitySelector => p => new()
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
        Parent = new Permission(p.Parent),
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
    };

    public Permission()
    {

    }
}
