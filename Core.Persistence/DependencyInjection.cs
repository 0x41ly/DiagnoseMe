using Core.Persistence.ServicesConfigrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        ConfigurationManager configuration
        )
    {
        services.AddDbContextConfiguration(configuration);
        services.AddIdentityConfiguration(configuration);
        services.AddRepositories();
        return services;
    }

    

        


}