using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using NPOI.POIFS.Properties;

namespace Standard.Tool.Platform.Data.Entities
{
    public class PermissionEntity
    {
        public PermissionEntity()
        {
            AccountPermissions = new HashSet<AccountPermissionEntity>();
            Childrens = new HashSet<PermissionEntity>();
        }

        /// <summary>
        /// GUID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public PermissionType Type { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

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
        [ForeignKey(nameof(ParentId))]
        [InverseProperty(nameof(Childrens))]
        public virtual PermissionEntity Parent { get; set; }

        /// <summary>
        /// 子级
        /// </summary>
        [InverseProperty(nameof(Parent))]
        public virtual ICollection<PermissionEntity> Childrens { get; set; }

        public virtual ICollection<AccountPermissionEntity> AccountPermissions { get; set; }
    }

    public enum PermissionType
    {
        [Description("系统")]
        System = 10,

        [Description("模块")]
        Module =0,

        [Description("页面")]
        Page = 1,

        [Description("按钮控制")]
        Control =2
    }

    internal class PermissionConfiguration : IEntityTypeConfiguration<PermissionEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionEntity> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.Name).HasMaxLength(64);
            builder.Property(e => e.Code).HasMaxLength(256);
            builder.HasOne(d => d.Parent).WithMany(p => p.Childrens)
            .HasForeignKey(d => d.ParentId);
        }
    }
}
