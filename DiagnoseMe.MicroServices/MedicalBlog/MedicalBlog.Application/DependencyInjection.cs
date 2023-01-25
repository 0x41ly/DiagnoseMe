using System.Reflection;
using FluentValidation;
using MediatR;
using MedicalBlog.Application.Common.Behaviors;
using MedicalBlog.Application.MiddlewaresConfigrations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalBlog.Application;

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