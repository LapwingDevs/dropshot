using DropShot.Application.Common;
using DropShot.Infrastructure.DAL;
using DropShot.Infrastructure.Identity.Helpers;
using DropShot.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DropShot.Infrastructure.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<DropShotDbContext>();
        
        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, DropShotDbContext>();
        
        services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
            
            options.User.RequireUniqueEmail = true;
        });
        services.Configure<AuthSettings>(configuration.GetSection("Authentication"));
        
        services.AddTransient<Application.Common.IAuthenticationService, AuthenticationService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<ITokenManager, TokenManager>();
        
        services.AddAuthentication()
            .AddIdentityServerJwt();
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
        });
        return services;
    }
}