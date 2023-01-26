namespace MedicalBlog.Domain.Entities;

public class PostRating : BaseEntity{
    public int Rating {get; set;}
    public string UserId {get; set;} = string.Empty;
    public string PostId {get; set;} = string.Empty;
    public virtual Post Post {set; get;} = new();
    public virtual User User {set; get;} = new();
}