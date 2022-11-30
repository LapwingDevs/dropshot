using DropShot.API.Configuration;
using DropShot.API.Middleware;
using DropShot.API.Services;
using DropShot.Application;
using DropShot.Application.Common;
using DropShot.Application.Common.Abstraction;
using DropShot.Infrastructure;
using DropShot.Infrastructure.Identity;
using DropShot.Infrastructure.Identity.Helpers;
using DropShot.Infrastructure.DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;

const string defaultCorsPolicy = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DropShotAPI", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Scheme = "bearer",
        Description = "Please insert JWT token into field"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            System.Array.Empty<string>()
        }
    });
});

if (builder.Configuration.GetValue<bool>("Testing"))
{
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "IntegrationTests";
        options.DefaultChallengeScheme = "IntegrationTests";
    }).AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
        "IntegrationTests", options => { });
}
else
{
    builder.Services.AddApplicationAuthentication(builder.Configuration);
}

var origins = builder.Configuration["origins"].Split(";");
builder.Services.AddCors(options =>
{
    options.AddPolicy(defaultCorsPolicy,
        corsPolicyBuilder =>
        {
            corsPolicyBuilder
                .WithOrigins(origins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

builder.Services.AddTransient<TokenManagerMiddleware>();
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();

var app = builder.Build();

if (app.Environment.IsProduction() == false)
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();

    using (var scope = app.Services.CreateScope())
    {
        var initializer = scope.ServiceProvider.GetRequiredService<DropShotDbContextInitializer>();

        if (builder.Configuration.GetValue<bool>("Testing") == false)
        {
            await initializer.InitDatabase();
        }

        await initializer.SeedDatabase();
    }
}

app.UseCors(defaultCorsPolicy);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

if (!builder.Configuration.GetValue<bool>("Testing"))
{
    app.UseMiddleware<TokenManagerMiddleware>();
}

app.UseInfrastructure();

app.Run();

public partial class Program { }
