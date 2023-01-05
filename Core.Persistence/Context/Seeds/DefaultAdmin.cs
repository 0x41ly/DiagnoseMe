using Core.Application.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Core.Persistence.Context.Seeds;


public class DefaultAdmin
{
 
    internal static ApplicationUser AdminUser(ConfigurationManager _config) {
        var user = new ApplicationUser
        {
            Id = "Admin".GetHashCode().ToString(),
            UserName = "Admin",
            FirstName = "Aly",
            LastName = "Khaled",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            Email = "alykhaled@diagnoseme.local",
            NormalizedEmail = "alykhaled@diagnoseme.local",
            NormalizedUserName = "0x41ly"
        };
        user.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(user,_config.GetValue<string>("AdminPassword"));
        return user;
    }

}