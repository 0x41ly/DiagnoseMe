using Auth.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Auth.Persistence.ServicesConfigrations;

public static class DbContextConfiguration
{
    public static IServiceCollection AddDbContextConfiguration(
        this IServiceCollection services, 
        ConfigurationManager configuration
        )
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection")!;
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)
            , options => options.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        services.AddScoped<DbContext, ApplicationDbContext>();

        return services;
    }
}