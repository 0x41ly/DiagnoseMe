using Microsoft.AspNetCore.Identity;

namespace Core.Shared.Entities;
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName {get; set;} = string.Empty;

    public DateTime LastConfirmationSentDate {get; set;}  
    public string Gender {get; set;}  = string.Empty;
    public DateTime DAteOfBirth {get; set;} 
    public string BloodType {get; set;}  = string.Empty;
    public bool IsDoctor {get; set;}
}