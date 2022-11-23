using DropShot.Application;
using DropShot.Infrastructure;
using DropShot.Infrastructure.DAL;

const string defaultCorsPolicy = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    .AddApplication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsProduction() == false)
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();

    using (var scope = app.Services.CreateScope())
    {
        var initializer = scope.ServiceProvider.GetRequiredService<DropShotDbContextInitializer>();
        await initializer.InitDatabase();
        await initializer.SeedDatabase();
    }
}

app.UseCors(defaultCorsPolicy);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseWebSockets();

app.Run();