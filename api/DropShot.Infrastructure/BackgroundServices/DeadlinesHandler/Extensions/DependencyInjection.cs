using DropShot.Application.Common;
using DropShot.Infrastructure.BackgroundServices.DeadlinesHandler.ExpiredDeadlinesCleaner;
using Microsoft.Extensions.DependencyInjection;

namespace DropShot.Infrastructure.BackgroundServices.DeadlinesHandler.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDeadlinesHandler(this IServiceCollection services)
    {
        services.AddHostedService<DeadlinesHandler>();

        services.AddTransient<IDeadlinesHandlerAccessor, DeadlinesHandlerAccessor>();
        services.AddTransient<IExpiredDeadlinesCleaner, ExpiredDeadlinesCleaner.ExpiredDeadlinesCleaner>();

        return services;
    }
}