using BloodDonation.Api.Common.Mapping;
using BloodDonation.Api.Common.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BloodDonation.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services)
    {
        services.AddMapping();
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory,BloodDonationProblemDetailsFactory>();
        return services;
    }
}