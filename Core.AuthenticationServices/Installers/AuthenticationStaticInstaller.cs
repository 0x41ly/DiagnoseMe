using Core.AuthenticationServices.Authentication;
using Core.AuthenticationServices.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.AuthenticationServices.Installers;
public static class AuthenticationStaticInstaller
{
    public static void AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IAuthentication<>), typeof(Authentication<>));
        services.AddScoped(typeof(UserManager<>), typeof(UserManager<>));
        services.Configure<Jwt>(configuration.GetSection("Jwt"));
    }
}