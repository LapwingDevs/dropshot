using DropShot.API.Configuration;
using DropShot.API.Middleware;
using DropShot.API.Services;
using DropShot.Application;
using DropShot.Application.Common;
using DropShot.Infrastructure;
using DropShot.Infrastructure.Identity;
using DropShot.Infrastructure.Identity.Helpers;
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

var origins = builder.Configuration["origins"].Split(";");
builder.Services.AddCors(options =>
{
    options.AddPolicy(defaultCorsPolicy,
        corsPolicyBuilder =>
        {
            corsPolicyBuilder
                .WithOrigins(origins)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddApplicationAuthentication(builder.Configuration);

builder.Services.AddTransient<TokenManagerMiddleware>();
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();

var app = builder.Build();

if (app.Environment.IsProduction() == false)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(defaultCorsPolicy);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<TokenManagerMiddleware>();
app.MapControllers();

app.Run();