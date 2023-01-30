using System;



using Microsoft.AspNetCore.Identity;

namespace Auth.Domain.Entities;
public class ApplicationUser : IdentityUser
{
    public ApplicationUser(){}
    public string FirstName { get; set; } = string.Empty;
    public string LastName {get; set;} = string.Empty;
    public string NationalID {get; set;} = string.Empty;

    public DateTime LastEmailChangeDate {get; set;}  
    public DateTime LastUserNameChangeDate {get; set;}  
    public DateTime LastConfirmationSentDate {get; set;}
    public string Gender {get; set;}  = string.Empty;
    public DateOnly DateOfBirth {get; set;} 
    public string BloodType {get; set;}  = string.Empty;
    public bool IsDoctor {get; set;}
    public string ProfilePictureUrl {get; set;} = string.Empty;

    public string? DoctorId {get; set;}
    public string? PatientId {get; set;}
}