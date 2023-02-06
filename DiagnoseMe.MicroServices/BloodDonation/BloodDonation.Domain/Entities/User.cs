using System;
namespace BloodDonation.Domain.Entities;


public class User : BaseEntity
{
    public string Name {get; set;} = string.Empty;
    public string FullName {get; set;} = string.Empty;
    public string ProfilePictureUrl {get; set;} = string.Empty;
}