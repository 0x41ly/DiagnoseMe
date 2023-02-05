namespace MedicalBlog.Domain.Entities;

public class UserSubscribedUser : BaseEntity
{
    public string UserId {get; set;} = string.Empty;
    public virtual User User {get; set;} = new();
    public string SubscribedUserId {get; set;} = string.Empty;
    public virtual User SubscribedUser {get; set;} = new();
}