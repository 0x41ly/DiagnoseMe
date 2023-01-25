using MedicalBlog.Application.Common.Interfaces.Services;
using MedicalBlog.Infrastructure.Services;
using MedicalBlog.Infrastructure.ServicesConfigrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalBlog.Infrastructue;

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