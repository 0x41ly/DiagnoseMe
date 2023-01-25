using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Auth.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    JwtSecurityToken GenerateJwtTokenAsync(string userId, string userName , List<Claim> userClaims);
}