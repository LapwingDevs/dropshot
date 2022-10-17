using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace DropShot.Infrastructure.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var postgresOptions = configuration.GetValue<PostgresOptions>("");
        
        services.AddDbContext<DropShotDbContext>(options => 
            options.UseNpgsql(postgresOptions.ConnectionString));

        return services;
    }
}