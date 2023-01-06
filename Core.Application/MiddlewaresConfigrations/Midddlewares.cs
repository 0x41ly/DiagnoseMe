using Core.Application.MiddlewaresConfigrations.Middlewares;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.Middleware;

public static class Midddlewares
{
    public static IServiceCollection AddMidddlewares(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddRateLimit(configuration);
        return services;
    }
}