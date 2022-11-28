using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.AuthenticationServices.Authentication
{
    public partial class Authentication<TUser> where TUser : IdentityUser
    {
        
        public virtual async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}