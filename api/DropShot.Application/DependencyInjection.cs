using System.Reflection;
using DropShot.Application.Carts;
using DropShot.Application.Carts.Interfaces;
using DropShot.Application.Auth;
using DropShot.Application.Auth.Interfaces;
using DropShot.Application.Drops;
using DropShot.Application.Drops.Interfaces;
using DropShot.Application.Orders;
using DropShot.Application.Orders.Interfaces;
using DropShot.Application.Products;
using DropShot.Application.Products.Interfaces;
using MediatR;
using DropShot.Application.Users;
using DropShot.Application.Users.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DropShot.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddTransient<IAuthService, AuthService>();

        services.AddTransient<ICartService, CartService>();

        services.AddTransient<IDropsService, DropsService>();
        services.AddTransient<IDropsListConverter, DropsListConverter>();

        services.AddTransient<IOrdersService, OrdersService>();

        services.AddTransient<IProductsService, ProductsService>();
        services.AddTransient<IVariantsService, VariantsService>();
        
        services.AddTransient<IUserService, UserService>();

        return services;
    }
}