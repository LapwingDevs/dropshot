using DropShot.Application.Common;
using DropShot.Application.Common.Abstraction;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DropShot.Infrastructure.WebSockets;

public static class DependencyInjection
{
    public static IServiceCollection AddWebSockets(this IServiceCollection services)
    {
        services.AddTransient<IDropsSocketPublisher, DropsSocketPublisher>();

        services.AddSignalR();
        
        return services;
    }

    public static WebApplication UseWebSockets(this WebApplication app)
    {
        app.MapHub<DropsHub>("/DropsSocket");

        return app;
    }
}