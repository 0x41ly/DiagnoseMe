using Auth.Persistence.ServicesConfigrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        ConfigurationManager configuration
        )
    {
        services.AddDbContextConfiguration(configuration);
        services.AddIdentityConfiguration(configuration);
        return services;
    }

    

        


}