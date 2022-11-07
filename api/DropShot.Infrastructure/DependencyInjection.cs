using DropShot.Application.Common;
using DropShot.Infrastructure.AppDateTime;
using DropShot.Infrastructure.BackgroundServices;
using DropShot.Infrastructure.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DropShot.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres(configuration);
        
        services.AddHostedService<DeadlinesChecker>();
        
        services.AddSingleton<IAppDateTime, UtcAppDateTime>();

        return services;
    }
}