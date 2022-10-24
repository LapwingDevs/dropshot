using System.Reflection;
using DropShot.Application.Drops;
using DropShot.Application.Drops.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DropShot.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddTransient<IDropsService, DropsService>();
        services.AddTransient<IDropsListConverter, DropsListConverter>();

        return services;
    }
}