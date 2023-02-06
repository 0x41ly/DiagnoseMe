using BloodDonation.Application.Common.Interfaces.Persistence;
using BloodDonation.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BloodDonation.Persistence.ServicesConfigrations;

public static class Repositories
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services)
        {
            
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
}