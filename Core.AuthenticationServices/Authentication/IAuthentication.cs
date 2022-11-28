using Core.AuthenticationServices.Models;
using Core.Shared.Settings;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Core.AuthenticationServices.Authentication
{
    public partial interface IAuthentication<TUser> where TUser : IdentityUser
    {
        Task<AuthenticationResults> RegisterAsync(TUser applicationUserDto, string password,MailSettings mailSettings);
        Task<AuthenticationResults> GetTokenAsync(Credentials credentials);
        Task<AuthenticationResults> ForgotPasswordAsync(string username,MailSettings mailSettings);
        Task<AuthenticationResults> ResetPasswordAsync(string username, string token, string newPassword);
        Task<AuthenticationResults> AddUserToRoleAsync(string username, string role);
        Task<AuthenticationResults> RemoveUserFromRoleAsync(string username, string role);
        List<TUser> GetAllUsers();
        Task<List<TUser>> GetUsersInRoleAsync(string role);
        Task<AuthenticationResults> ConfirmEmailAsync(string username, string token);
        Task SignOutAsync();
        Task<AuthenticationResults> ChangeEmailAsync(string username, string newEmail, MailSettings mailSettings);
        Task<AuthenticationResults> ChangeNameAsync(string username, string newName);
        Task<AuthenticationResults> ConfirmEmailChangeAsync(string username, string email, string token);
        Task<AuthenticationResults> ResendEmailConfirmationAsync(string username, MailSettings mailSettings);
    }
}