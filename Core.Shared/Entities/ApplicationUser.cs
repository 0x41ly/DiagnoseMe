using Microsoft.AspNetCore.Identity;

namespace Core.Shared.Entities;
public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}