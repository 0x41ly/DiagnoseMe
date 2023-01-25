using AspNetCoreRateLimit;
using Auth.Api;
using Auth.Application;
using Auth.Infrastructue;
using Auth.Persistence;
using Auth.Persistence.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


{
    builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
    builder.Services
        .AddPresentation()
        .AddApplication(builder.Configuration)
        .AddPersistence(builder.Configuration)
        .AddInfrastrucure(builder.Configuration);
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