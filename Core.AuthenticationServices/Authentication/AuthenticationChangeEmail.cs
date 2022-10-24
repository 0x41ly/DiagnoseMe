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
        public virtual async Task<AuthenticationResults> ChangeEmailAsync(string username, string newEmail, string url)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(user.EmailConfirmed)
            {
                var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
                token = HttpUtility.UrlEncode(token);
                try {
                    await Smtp.SendEmailAsync(user.Email, "Email verification", $"Please confirm your E-mail by clicking this link: {"\n"} {url}/api/Auth/ConfirmEmailChange?username={user.UserName}&newEmail={newEmail}&token={token}");
                    return new AuthenticationResults
                    {
                        IsSuccess = true,
                        Message = $"We sent you an email to {newEmail}.\nPlease confirm your new email by clicking on a link in email we sent you.",
                        Username = user.UserName,
                        Roles = new List<string>() { Roles.User },
                    };
                }
                catch{
                    return new AuthenticationResults
                    {
                        
                        Message = "Failed to send email"
                    };
                }

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