using BloodDonation.Application.Common.Interfaces.Services;
using BloodDonation.Infrastructure.Services;
using BloodDonation.Infrastructure.ServicesConfigrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BloodDonation.Infrastructue;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastrucure(
        this IServiceCollection services,
        ConfigurationManager configuration
        )
    {
        services.AddAuthentication(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IFileHandler, FileHandler>();
        return services;
    }
}