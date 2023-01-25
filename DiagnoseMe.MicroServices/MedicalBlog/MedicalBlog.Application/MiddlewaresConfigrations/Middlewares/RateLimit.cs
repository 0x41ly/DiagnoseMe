using Microsoft.Extensions.DependencyInjection;
using AspNetCoreRateLimit;
using Microsoft.Extensions.Configuration;

namespace MedicalBlog.Application.MiddlewaresConfigrations.Middlewares;


public static class RateLimit
{
    public static IServiceCollection AddRateLimit(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddMemoryCache();   
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddInMemoryRateLimiting();
        services.Configure<IpRateLimitOptions>(options => 
            configuration.
            GetSection("IpRateLimitingSettings").
            Bind(options));
        return services;
    }
}