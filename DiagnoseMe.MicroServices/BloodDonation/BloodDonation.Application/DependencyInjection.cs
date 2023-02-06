using System.Reflection;
using FluentValidation;
using MediatR;
using BloodDonation.Application.Common.Behaviors;
using BloodDonation.Application.MiddlewaresConfigrations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BloodDonation.Application;

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
        services.AddScoped(
            typeof(IPipelineBehavior<,>), 
            typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMiddlewares(configuration);
        return services;
    }
}