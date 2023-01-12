using System.Reflection;
using Core.Api.Common.Errors;
using Core.Api.Common.Mapping;

using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Core.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services)
    {
        services.AddMapping();
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory,CoreProblemDetailsFactory>();
        return services;
    }
}