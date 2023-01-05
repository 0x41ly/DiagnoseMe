using Core.Domain.Common.Roles;
using Microsoft.AspNetCore.Identity;

namespace Core.Persistence.Context.Seeds;

public static class DefaultRoles
{
    internal static List<IdentityRole> IdentityRoleList()
        {
            return new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id = Roles.Admin.GetHashCode().ToString(),
                    Name = Roles.Admin,
                    NormalizedName = Roles.Admin
                },
                new IdentityRole
                {
                    Id = Roles.User.GetHashCode().ToString(),
                    Name = Roles.User,
                    NormalizedName = Roles.User
                },
                new IdentityRole
                {
                    Id = Roles.Doctor.GetHashCode().ToString(),
                    Name = Roles.Doctor,
                    NormalizedName = Roles.Doctor
                }
            };
        }
}