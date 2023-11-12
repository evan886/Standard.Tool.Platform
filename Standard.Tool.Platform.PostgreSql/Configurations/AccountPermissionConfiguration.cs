using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Standard.Tool.Platform.Data.Entities;

namespace Standard.Tool.Platform.PostgreSql.Configurations;

internal class AccountPermissionConfiguration : IEntityTypeConfiguration<AccountPermissionEntity>
{
    public void Configure(EntityTypeBuilder<AccountPermissionEntity> builder)
    {

        builder.HasKey(t => new { t.AccountId, t.PermissionId });

        builder.HasOne(pt => pt.Account)
            .WithMany(p => p.AccountPermissions)
            .HasForeignKey(pt => pt.AccountId)
            .HasConstraintName("FK_PostCategory_Account");

        builder.HasOne(pt => pt.Permission)
            .WithMany(t => t.AccountPermissions)
            .HasForeignKey(pt => pt.PermissionId)
            .HasConstraintName("FK_PostCategory_Permission");
    }
}
