using Auth.Application.Common.Interfaces.Services;
using Auth.Domain.Common.Roles;
using Auth.Domain.Common.Users;
using Microsoft.AspNetCore.Identity;

namespace Auth.Persistence.Context.Seeds;


public  class DefaultAdminRole
{

    
    internal static IdentityUserRole<string> Role=> new IdentityUserRole<string>{
        RoleId = Roles.AdminId,
        UserId = Users.AdminId
    };
}