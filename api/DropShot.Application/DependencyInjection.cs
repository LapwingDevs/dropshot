﻿using System.Reflection;
using DropShot.Application.Carts;
using DropShot.Application.Carts.Interfaces;
using DropShot.Application.Drops;
using DropShot.Application.Drops.Interfaces;
using DropShot.Application.Products;
using DropShot.Application.Products.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DropShot.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddTransient<ICartService, CartService>();

        services.AddTransient<IDropsService, DropsService>();
        services.AddTransient<IDropsListConverter, DropsListConverter>();

        services.AddTransient<IProductsService, ProductsService>();
        services.AddTransient<IVariantsService, VariantsService>();

        return services;
    }
}