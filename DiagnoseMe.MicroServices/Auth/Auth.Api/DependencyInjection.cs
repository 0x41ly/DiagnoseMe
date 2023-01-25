using Auth.Api.Common.Errors;
using Auth.Api.Common.Mapping;

using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Auth.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services)
    {
        services.AddMapping();
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory,AuthProblemDetailsFactory>();
        return services;
    }
}