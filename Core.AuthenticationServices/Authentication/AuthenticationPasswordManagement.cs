using System.Web;
using Core.AuthenticationServices.Models;
using EmailSender;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Core.AuthenticationServices.Authentication
{
    public partial class Authentication<TUser> where TUser : IdentityUser
    {
        public virtual async Task<AuthenticationResults> ForgotPasswordAsync(string username, string url = null!)
        {
            var results = new AuthenticationResults();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                results.Message = "User does not exist";
                return results;
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //it has to be url encoded so that it can be sent in the url
            token = HttpUtility.UrlEncode(token);
            
            try
            {
                await Smtp.SendEmailAsync(user.Email, "Reset Password", $"Please reset your password by visiting this link: {"\n"} {url}/Auth/ResetPassword?username={username}&token={token}");
                results.Message = "Email sent successfully";
                results.IsSuccess = true;
                results.Username = username;
                results.Roles = await _userManager.GetRolesAsync(user) as List<string>;
                return results;
            }
            catch
            {
                results.Message = "Failed to send email";
                return results;
            }
        }
        public virtual async Task<AuthenticationResults> ResetPasswordAsync(string username, string token, string password)
        {
            var results = new AuthenticationResults();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                results.Message = "User does not exist";
                return results;
            }
            var result = await _userManager.ResetPasswordAsync(user, token, password);
            if (!result.Succeeded)
            {
                results.Message = "Failed to reset password";
                return results;
            }
            results.IsSuccess = true;
            results.Message = "Password reset successfully";
            return results;
        }
    }
}
