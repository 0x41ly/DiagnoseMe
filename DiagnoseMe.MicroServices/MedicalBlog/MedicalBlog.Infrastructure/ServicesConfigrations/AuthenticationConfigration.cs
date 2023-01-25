using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MedicalBlog.Infrastructure.Authentication;

namespace MedicalBlog.Infrastructure.ServicesConfigrations;

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