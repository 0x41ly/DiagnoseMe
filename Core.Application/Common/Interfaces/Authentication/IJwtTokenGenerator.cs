using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    JwtSecurityToken GenerateJwtTokenAsync(string userId, string userName , List<Claim> userClaims);
}