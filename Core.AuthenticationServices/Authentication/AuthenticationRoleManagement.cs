using Core.AuthenticationServices.Helpers;
using Core.AuthenticationServices.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Core.AuthenticationServices.Authentication
{
    public partial class Authentication<TUser> where TUser : IdentityUser
    {
        public virtual async Task<AuthenticationResults> AddUserToRoleAsync(string username, string role)
        {
            var results = new AuthenticationResults();
            if (!Roles.AvailableRoles.Contains(role))
            {
                results.Message = "Role does not exist";
                return results;
            }
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                results.Message = "User does not exist";
                return results;
            }

            var result = await _userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded)
            {
                results.Message = "Failed to add user to role";
                return results;
            }
            results.IsSuccess = true;
            results.Message = "User added to role successfully";
            results.Username = username;
            results.Roles = await _userManager.GetRolesAsync(user) as List<string>;
            return results;
        }

        public async Task<AuthenticationResults> RemoveUserFromRoleAsync(string username, string role)
        {
            var results = new AuthenticationResults();
            if (!Roles.AvailableRoles.Contains(role))
            {
                results.Message = "Role does not exist";
                return results;
            }
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                results.Message = "User does not exist";
                return results;
            }

            var result = await _userManager.RemoveFromRoleAsync(user, role);
            if (!result.Succeeded)
            {
                results.Message = "Failed to remove user from role";
                return results;
            }
            results.IsSuccess = true;
            results.Message = "User removed from role successfully";
            results.Username = username;
            results.Roles = await _userManager.GetRolesAsync(user) as List<string>;
            return results;
        }
    }
}
