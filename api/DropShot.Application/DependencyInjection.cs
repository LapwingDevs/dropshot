using System.Reflection;
using DropShot.Application.Auth;
using DropShot.Application.Auth.Interfaces;
using DropShot.Application.Drops;
using DropShot.Application.Drops.Interfaces;
using DropShot.Application.Products;
using DropShot.Application.Products.Interfaces;
using DropShot.Application.User;
using DropShot.Application.User.Interfaces;
using DropShot.Application.Users;
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

        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IProductsService, ProductsService>();
        services.AddTransient<IVariantsService, VariantsService>();
        services.AddTransient<IUserService, UserService>();

        return services;
    }
}