using DiagnoseMe.GateWay.MiddlewaresConfigrations;
using Ocelot.DependencyInjection;

namespace DiagnoseMe.GateWay;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddOcelot(configuration);
        services.AddMiddlewares(configuration);
        return services;
    }
}