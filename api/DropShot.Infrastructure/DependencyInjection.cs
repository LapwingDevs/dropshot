using DropShot.Application.Common;
using DropShot.Infrastructure.AppDateTime;
using DropShot.Infrastructure.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DropShot.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres(configuration);

        services.AddSingleton<IAppDateTime, UtcAppDateTime>();

        return services;
    }
}