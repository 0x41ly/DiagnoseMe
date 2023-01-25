
namespace Auth.Application.Authentication.Common;



public record AuthenticationResults
{
    public string? Message { get; set; } 
    public string? Token { get; set; } 
    public string? Username { get; set; }
}
