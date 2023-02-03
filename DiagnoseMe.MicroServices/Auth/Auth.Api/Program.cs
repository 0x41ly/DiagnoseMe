using AspNetCoreRateLimit;
using Auth.Api;
using Auth.Application;
using Auth.Infrastructue;
using Auth.Persistence;
using Auth.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


{
    builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
    builder.Services
        .AddPresentation()
        .AddApplication(builder.Configuration)
        .AddPersistence(builder.Configuration)
        .AddInfrastrucure(builder.Configuration);
    Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File("logs/authapi-.txt", rollingInterval: RollingInterval.Day)
                    .MinimumLevel.Debug()
                    .CreateLogger();
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(Log.Logger);
    Serilog.ILogger logger = Log.Logger.ForContext<Program>();
    builder.Services.AddSingleton<Serilog.ILogger>(logger);
    logger.Information("Starting up");
}

var app = builder.Build();


{
    using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope())
    {
        var context = serviceScope?.ServiceProvider.GetRequiredService<ApplicationDbContext>()!;

        context.Database.Migrate();

    }
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseIpRateLimiting();
    app.UseExceptionHandler("/error");
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}