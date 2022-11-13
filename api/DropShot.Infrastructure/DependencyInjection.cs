﻿using DropShot.Application.Common;
using DropShot.Infrastructure.AppDateTime;
using DropShot.Infrastructure.BackgroundServices;
using DropShot.Infrastructure.BackgroundServices.DeadlinesHandler;
using DropShot.Infrastructure.BackgroundServices.DeadlinesHandler.Extensions;
using DropShot.Infrastructure.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DropShot.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres(configuration);

        services.AddDeadlinesHandler();

        services.AddSingleton<IAppDateTime, UtcAppDateTime>();

        return services;
    }
}