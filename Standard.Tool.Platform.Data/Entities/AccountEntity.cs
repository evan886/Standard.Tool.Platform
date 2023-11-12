using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Standard.Tool.Platform.Data.Entities;

public class AccountEntity
{
    public AccountEntity()
    {
        AccountPermissions = new HashSet<AccountPermissionEntity>();
    }

    public Guid Id { get; set; }

    public string LoginName { get; set; }

    public string UserName { get; set; }

    public string PasswordHash { get; set; }

    public DateTime? LastLoginTimeUtc { get; set; }

    public DateTime CreateTimeUtc { get; set; }

    public string Status { get; set; }

    public virtual ICollection<AccountPermissionEntity> AccountPermissions { get; set; }
}

//internal class AccountConfiguration : IEntityTypeConfiguration<AccountEntity>
//{
//    public void Configure(EntityTypeBuilder<AccountEntity> builder)
//    {
//        builder.Property(e => e.Id).ValueGeneratedNever();
//        builder.Property(e => e.LoginName).HasMaxLength(64);
//        builder.Property(e => e.UserName).HasMaxLength(64);
//        builder.Property(e => e.PasswordHash).HasMaxLength(128);
//        builder.Property(e => e.Status).HasMaxLength(64);
//    }
//}
