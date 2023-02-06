using MedicalBlog.Api.Common.Mapping;
using MedicalBlog.Api.Common.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MedicalBlog.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services)
    {
        services.AddMapping();
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory,MedicalBlogProblemDetailsFactory>();
        return services;
    }
}