using Core.AuthenticationServices.Helpers;
using Core.AuthenticationServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailSender;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Core.Shared.Settings;
using System.Net.Mail;

namespace Core.AuthenticationServices.Authentication
{
    public partial class Authentication<TUser> where TUser : IdentityUser
    {
        public virtual async Task<AuthenticationResults> RegisterAsync(TUser applicationUserDto, string password, MailSettings mailSettings)
        {
            if (await _userManager.FindByEmailAsync(applicationUserDto.Email) != null)
                return new AuthenticationResults
                {
                    Message = "Email is already in use",
                };
            if (await _userManager.FindByNameAsync(applicationUserDto.UserName) != null)
                return new AuthenticationResults
                {
                    Message = "Username is already in use",
                };
            var user = _mapper.Map<TUser>(applicationUserDto);
            var result = await _userManager.CreateAsync(user, password);
            var errorMessages = result.Errors.Select(e => e.Description);
            if (!result.Succeeded)
                return new AuthenticationResults
                {
                    Message = String.Join(" , ", errorMessages),
                };
            await _userManager.AddToRoleAsync(user, Roles.User);
            return await SendEmailAsync(user, mailSettings);
        }

        private async Task<AuthenticationResults> SendEmailAsync(TUser user,MailSettings mailSettings)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = HttpUtility.UrlEncode(token);
            try
            {
                await Smtp.SendEmailAsync(
                    new MailAddress(user.Email,user.UserName),
                    "Email verification",
                    $"Please verify your E-mail by clicking this link: {"\n"} https://{DomainSettings.DomainName}/api/Auth/ConfirmEmail?username={user.UserName}&token={token}",
                    mailSettings
                    );
                return new AuthenticationResults
                {
                    IsSuccess = true,
                    Message = $"We sent you an email to {user.Email}.\nPlease activate your account by clicking on a link in email we sent you.",
                    Username = user.UserName,
                    Roles = new List<string>() { Roles.User },
                };
            }
            catch
            {
                return new AuthenticationResults
                {
                    IsSuccess = false,
                    Message = "Failed to send email"
                };
            }
        }
    }
}
