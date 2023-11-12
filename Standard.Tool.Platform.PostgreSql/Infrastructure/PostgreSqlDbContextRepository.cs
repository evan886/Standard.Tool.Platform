using Standard.Tool.Platform.Data.Infrastructure;

namespace Standard.Tool.Platform.PostgreSql.Infrastructure;

public class PostgreSqlDbContextRepository<T> : DbContextRepository<T> where T : class
{
    public PostgreSqlDbContextRepository(PostgreSqlToolsBlockDbContext dbContext)
        : base(dbContext)
    {
    }
}
