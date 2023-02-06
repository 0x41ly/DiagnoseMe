using BloodDonation.Application.MiddlewaresConfigrations.Middlewares;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BloodDonation.Application.MiddlewaresConfigrations;

public static class MiddlewaresConfigrations
{
    public static IServiceCollection AddMiddlewares(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddRateLimit(configuration);
        return services;
    }
}