using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Core.AuthenticationServices.Authentication
{
    public partial class Authentication<TUser> where TUser : IdentityUser
    {
        protected virtual async Task<JwtSecurityToken> GenerateJwtTokenAsync(TUser user)
        {

            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            var RoleClaims = new List<Claim>();
            foreach (var role in userRoles)
            {
                RoleClaims.Add(new Claim("roles", role));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
            }
            .Union(userClaims)
            .Union(RoleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresAt = DateTime.Now.AddDays(_jwt.DurationInDays);
            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials
            );
            return token;
        }
    }
}
