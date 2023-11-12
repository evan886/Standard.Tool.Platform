using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using Standard.Tool.Platform.Data.Infrastructure;

namespace Standard.Tool.Platform.Oracle;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOracleStorage(this IServiceCollection services, string connectionString)
    {
        services.AddScoped(typeof(IOracleRepository<>), typeof(OracleDbContextRepository<>));

        // 配置SqlSugar  
        services.AddScoped<SqlSugarClient>(x =>
        {
            var client = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = connectionString,
                DbType = DbType.Oracle,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            return client;
        });

        return services;
    }
}
