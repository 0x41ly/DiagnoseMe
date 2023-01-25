using System.Text;
using Auth.Application.Common.Interfaces.Authentication;
using Auth.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Infrastructure.ServicesConfigrations;

public static class Authentication
{
    public static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        ConfigurationManager configuration
        )
        {
            var JwtSettings = new JwtSettings();
            configuration.Bind("JwtSettings",JwtSettings);
            services.AddSingleton(Options.Create(JwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddAuthentication(
                JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, 
                options =>
                        {
                            options.ForwardDefault = JwtBearerDefaults.AuthenticationScheme;
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,
                                ValidIssuer = JwtSettings.Issuer,
                                ValidAudience = JwtSettings.Audience,
                                IssuerSigningKey = new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(JwtSettings.Key)
                                    )};
                        });


            return services;
        }
}