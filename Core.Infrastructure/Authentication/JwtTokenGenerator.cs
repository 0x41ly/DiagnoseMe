using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Application.Common.Interfaces.Authentication;
using Core.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Core.Infrastructure.Authentication;

public class JwtTokenGenerator  : IJwtTokenGenerator
{

    private readonly JwtSettings _jwtSettings;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtTokenGenerator(
        IOptions<JwtSettings> jwtSettings,
        IDateTimeProvider dateTimeProvider
        )
        {
        _jwtSettings = jwtSettings.Value;
        _dateTimeProvider = dateTimeProvider;
        }
    public JwtSecurityToken GenerateJwtTokenAsync(string userId, string userName, List<Claim> userClaims)
    {
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, userName),
        }
        .Union(userClaims);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiresAt = _dateTimeProvider.UtcNow.AddMinutes(60);
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials
        );
        return token;
    }


}