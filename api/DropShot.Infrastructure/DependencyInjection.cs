using DropShot.Application.Common;
using DropShot.Application.Common.Abstraction;
using DropShot.Infrastructure.AppDateTime;
using DropShot.Infrastructure.BackgroundServices.DeadlinesHandler.Extensions;
using DropShot.Infrastructure.DAL;
using DropShot.Infrastructure.WebSockets;
using DropShot.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace DropShot.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres(configuration);

        services.AddDeadlinesHandler();

        services.AddIdentity(configuration);
        
        services.AddSingleton<IAppDateTime, UtcAppDateTime>();

        services.AddWebSockets();

        return services;
    }
    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseWebSockets();
        
        return app;
    }
}