using DropShot.Application.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DropShot.Infrastructure.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("Postgres:ConnectionString");
        services.AddDbContext<DropShotDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IDbContext>(provider => provider.GetRequiredService<DropShotDbContext>());

        return services;
    }
}