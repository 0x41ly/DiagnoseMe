using System.Reflection;
using Auth.Application.Common.Behaviors;
using Auth.Application.MiddlewaresConfigrations;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Application;

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