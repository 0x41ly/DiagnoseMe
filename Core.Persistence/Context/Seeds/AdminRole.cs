using Core.Domain.Common.Roles;
using Microsoft.AspNetCore.Identity;

namespace Core.Persistence.Context.Seeds;


public class DefaultAdminRole
{
    internal static IdentityUserRole<string> Role => new IdentityUserRole<string>{
        RoleId = Roles.Admin.GetHashCode().ToString(),
        UserId = "Admin".GetHashCode().ToString()
    };
}