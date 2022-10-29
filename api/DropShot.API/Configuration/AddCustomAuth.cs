using System.Text;
using DropShot.Infrastructure.Identity.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace DropShot.API.Configuration;

public static class AddCustomAuth
{
    public static void AddApplicationAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var settingsSectionAuth = configuration.GetSection("Authentication");
        var authSettings = settingsSectionAuth.Get<AuthSettings>();

        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };

                //SET ONLY IN-DEV TODO: make this automatic
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.AuthKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = authSettings.Issuer,
                    ValidAudience = authSettings.Audience,
                    ValidateLifetime = true
                };
            });
    }
}