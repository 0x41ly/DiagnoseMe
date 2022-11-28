using System.Threading.Tasks;
using Core.AuthenticationServices.Models;
using Core.Shared.Settings;
using Microsoft.AspNetCore.Identity;

namespace Core.AuthenticationServices.Authentication
{
    public partial class Authentication<TUser> where TUser : IdentityUser
    {
        public virtual async Task<AuthenticationResults> ResendEmailConfirmationAsync(string username,MailSettings mailSettings)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(!user.EmailConfirmed)
            {
               return await SendEmailAsync(user,mailSettings);
            }
            return new AuthenticationResults
            {
                Message = "Email is already confirmied"
            };
        }
    }
}