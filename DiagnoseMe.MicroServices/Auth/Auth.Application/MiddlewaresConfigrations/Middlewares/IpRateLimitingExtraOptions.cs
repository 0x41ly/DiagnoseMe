namespace Auth.Application.MiddlewaresConfigrations.Middlewares;

public class IpRateLimitingExtraOptions
{
    public List<string> HostWhiteList { get; set; } = new();
}