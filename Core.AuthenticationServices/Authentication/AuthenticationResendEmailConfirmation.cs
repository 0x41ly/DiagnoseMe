using System.Collections.Generic;
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
        public virtual async Task<AuthenticationResults> ResendEmailConfirmationAsync(string username, string url)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(!user.EmailConfirmed)
            {
               return await SendEmailAsync(url,user);
            }
            return new AuthenticationResults
            {
                Message = "Email is already confirmied"
            };
        }
    }
}