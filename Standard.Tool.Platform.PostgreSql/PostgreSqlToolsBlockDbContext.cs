using Microsoft.EntityFrameworkCore;
using Standard.Tool.Platform.Data;
using Standard.Tool.Platform.PostgreSql.Configurations;
using System;
using System.Reflection.Emit;

namespace Standard.Tool.Platform.PostgreSql
{
    public class PostgreSqlToolsBlockDbContext : ToolsBlockDbContext
    {
        public PostgreSqlToolsBlockDbContext()
        {
        }

        public PostgreSqlToolsBlockDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountPermissionConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
