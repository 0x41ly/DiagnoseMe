using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Web;
using Core.AuthenticationServices.Helpers;
using Core.AuthenticationServices.Models;
using EmailSender;
using Microsoft.AspNetCore.Identity;

namespace Core.AuthenticationServices.Authentication
{
    public partial class Authentication<TUser> where TUser : IdentityUser
    {
        public virtual async Task<AuthenticationResults> ChangeNameAsync(string username, string newName)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(user.EmailConfirmed)
            {
                if (await _userManager.FindByNameAsync(newName) != null)
                    return new AuthenticationResults
                    {
                        Message = "Username is already in use",
                    };
                var setUserNameResult = await _userManager.SetUserNameAsync(user, newName);
                if (!setUserNameResult.Succeeded)
                {
                    return new AuthenticationResults{
                        Message= "Unable to change your username"
                    };
                }
                return new AuthenticationResults{
                    Message= "Successfully changed your username",
                    IsConfirmed= true,
                    IsSuccess=true,
                    Token =  new JwtSecurityTokenHandler().WriteToken(await GenerateJwtTokenAsync(user)),
                    Username = user.UserName,
                    Roles = new List<string>() { Roles.User }
                };
            }
            else
            {
                return new AuthenticationResults
                {
                    Message = "Confirm your email first"
                };
            }
        }
    }
}