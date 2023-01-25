using Auth.Domain.Common.Roles;
using Microsoft.AspNetCore.Identity;

namespace Auth.Persistence.Context.Seeds;

public static class DefaultRoles
{
    internal static List<IdentityRole> IdentityRoleList()
        {
            return new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id = Roles.AdminId,
                    Name = Roles.Admin,
                    NormalizedName = Roles.Admin
                },
                new IdentityRole
                {
                    Id = Roles.UserId,
                    Name = Roles.User,
                    NormalizedName = Roles.User
                },
                new IdentityRole
                {
                    Id = Roles.DoctorId,
                    Name = Roles.Doctor,
                    NormalizedName = Roles.Doctor
                }
            };
        }
}