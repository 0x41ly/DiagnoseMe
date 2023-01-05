using Core.Application.Common.Interfaces.Email;
using Core.Application.Common.Interfaces.Services;
using Core.Application.Settings;
using Core.Infrastructure.Domain;
using Core.Infrastructure.Email;
using Core.Infrastructure.Services;
using Core.Infrastructure.ServicesConfigrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructue;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastrucure(
        this IServiceCollection services,
        ConfigurationManager configuration
        )
    {
        services.AddAuthentication(configuration);
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.Configure<DomainSettings>(configuration.GetSection("DomainSettings"));
        services.AddSingleton<ISmtp, Smtp>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    

        


}