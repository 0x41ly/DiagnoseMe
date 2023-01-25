using System.Security.Claims;
using System.Security.Cryptography;

namespace Auth.Application.Authentication;


public class BaseAuthenticationHandler
{
    protected readonly UserManager<ApplicationUser> _userManager;

    protected BaseAuthenticationHandler(
        UserManager<ApplicationUser> userManager
    )
    {
        _userManager = userManager;
    }


    protected async Task<List<Claim>> GetUserClaims(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);
        var RoleClaims = new List<Claim>();
        foreach (var role in userRoles)
        {
            RoleClaims.Add(new Claim("roles", role));
        }
        var claims = userClaims.Union(RoleClaims).ToList();
        return claims;
    }
    protected string GenerateRandomPin()
    {
        var bytes = new byte[10];
        using (var rng = RandomNumberGenerator.Create()){
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes).Substring(0,10);
        }
    }

}