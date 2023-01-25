
namespace DiagnoseMe.GateWay.MiddlewaresConfigrations;

public static class MiddlewaresConfigrations
{
    public static IServiceCollection AddMiddlewares(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        return services;
    }
}