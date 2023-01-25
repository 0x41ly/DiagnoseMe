using Auth.Application.Common.Interfaces.Email;
using Auth.Application.Common.Interfaces.Services;
using Auth.Application.Settings;
using Auth.Infrastructure.Email;
using Auth.Infrastructure.Services;
using Auth.Infrastructure.ServicesConfigrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructue;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastrucure(
        this IServiceCollection services,
        ConfigurationManager configuration
        )
    {
        services.AddAuthentication(configuration);
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddSingleton<ISmtp, Smtp>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IFileHandler, FileHandler>();
        return services;
    }

    

        


}