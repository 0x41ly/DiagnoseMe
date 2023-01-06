using Core.Application.Middleware;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        ConfigurationManager configuration
        )
    {
        services.AddSwaggerGenConfiguration(configuration);
        services.AddSingleton<IMemoryCache,MemoryCache>();
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddMidddlewares(configuration);
        return services;
    }
}