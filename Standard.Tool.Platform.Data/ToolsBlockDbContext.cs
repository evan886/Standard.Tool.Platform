using Microsoft.EntityFrameworkCore;
using Standard.Tool.Platform.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Data
{
    public class ToolsBlockDbContext:DbContext
    {
        public ToolsBlockDbContext()
        {
        }

        public ToolsBlockDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<MenuEntity> Menu { get; set; }

        public virtual DbSet<SubMenuEntity> SubMenu { get; set; }

        public virtual DbSet<AccountPermissionEntity> AccountPermission { get; set; }

        public virtual DbSet<AccountEntity> Account { get; set; }

        public virtual DbSet<PermissionEntity> Permission { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new SubMenuConfiguration());

        }
    }

    public static class ToolsBlockDbContextExtension
    {
        public static async Task ClearAllData(this ToolsBlockDbContext context)
        {
            context.AccountPermission.RemoveRange();
            context.Account.RemoveRange();
            await context.SaveChangesAsync();
        }
    }
}
