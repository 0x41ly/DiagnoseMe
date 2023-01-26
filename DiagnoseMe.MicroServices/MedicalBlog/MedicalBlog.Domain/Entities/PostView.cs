namespace MedicalBlog.Domain.Entities;

public class PostView : BaseEntity
{
    public string UserId {get; set;} = string.Empty;
    public string PostId {get; set;} = string.Empty;
    public virtual Post Post {set; get;} = new();
    public virtual User User {set; get;} = new();
}