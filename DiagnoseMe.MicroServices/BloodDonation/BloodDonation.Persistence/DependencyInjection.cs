using BloodDonation.Persistence.ServicesConfigrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BloodDonation.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        ConfigurationManager configuration
        )
    {
        services.AddRepositories();
        services.AddDbContextConfiguration(configuration);
        return services;
    }

    

        


}