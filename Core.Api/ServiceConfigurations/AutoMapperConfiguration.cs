
using Core.Shared.AutoMapperProfiles;

namespace Core.Api.ServiceConfigurations;

public static class AutoMapperConfiguration
{
    public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MailMessageProfile).Assembly);
        return services;
    }
}