


using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Entities;
public class ApplicationUser : IdentityUser
{
    public ApplicationUser(){
        SentChats = new HashSet<Chat>();
        RecievedChats = new HashSet<Chat>();
    }
    public string FirstName { get; set; } = string.Empty;
    public string LastName {get; set;} = string.Empty;
    public string NationalID {get; set;} = string.Empty;

    public DateTime LastConfirmationSentDate {get; set;}  
    public string Gender {get; set;}  = string.Empty;
    public DateTime DateOfBirth {get; set;} 
    public string BloodType {get; set;}  = string.Empty;
    public bool IsDoctor {get; set;}
    public string ProfilePic {get; set;} = string.Empty;

    
    public virtual ICollection<Chat> SentChats {get; set;} 
    public virtual ICollection<Chat> RecievedChats {get; set;} 
    public virtual Doctor? Doctor {get; set;}
    public virtual Patient? Patient {get; set;}

 
}