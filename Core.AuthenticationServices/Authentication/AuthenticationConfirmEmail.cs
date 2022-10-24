

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Core.AuthenticationServices.Helpers;
using Core.AuthenticationServices.Models;
using Microsoft.AspNetCore.Identity;

namespace Core.AuthenticationServices.Authentication
{
    public partial class Authentication<TUser> where TUser : IdentityUser
    {
        public virtual async Task<AuthenticationResults> ConfirmEmailAsync(string username, string token)
        {
            var results = new AuthenticationResults{};
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                results.Message = "User does not exist";
                return results;
            }
            var result = await _userManager.ConfirmEmailAsync(user,token);
            if (!result.Succeeded)
            {
                results.Message = "Failed to find user";
                return results;
            }
            return new AuthenticationResults{
                IsSuccess = true,
                Message = "Email confirmed successfully",
                Token =  new JwtSecurityTokenHandler().WriteToken(await GenerateJwtTokenAsync(user)),
                Username = user.UserName,
                Roles = new List<string>() { Roles.User },
                IsConfirmed = true
            };
        }
    }
}