using Microsoft.Extensions.DependencyInjection;
using AspNetCoreRateLimit;
using Microsoft.Extensions.Configuration;
using System.Net;
using Microsoft.Extensions.Options;

namespace BloodDonation.Application.MiddlewaresConfigrations.Middlewares;


public static class RateLimit
{
    public static IServiceCollection AddRateLimit(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddMemoryCache();   
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddInMemoryRateLimiting();
        var ipRateLimitOptions = new IpRateLimitOptions();
        configuration.Bind("IpRateLimitingSettings",ipRateLimitOptions);
        var ipRateLimitingExtraOptions = new IpRateLimitingExtraOptions();
        configuration.Bind("IpRateLimitingExtraOptions", ipRateLimitingExtraOptions);
        if(ipRateLimitOptions.IpWhitelist == null)
            ipRateLimitOptions.IpWhitelist = new List<string>();
        foreach(var hostname in ipRateLimitingExtraOptions.HostWhiteList!)
        {
            IPAddress[] iPAddresses = Dns.GetHostAddresses(hostname);
            ipRateLimitOptions.IpWhitelist.Add(string.Join(",", iPAddresses.Select(x => x.ToString())));
        }
        services.AddSingleton(Options.Create(ipRateLimitOptions));
        return services;
    }
}