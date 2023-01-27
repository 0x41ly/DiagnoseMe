namespace MedicalBlog.Domain.Entities;

public class Post : BaseEntity{
    public Post()
    {
        PostRatings = new HashSet<PostRating>();
        Comments = new HashSet<Comment>();
        PostViews = new HashSet<PostView>();
    }
    public string Title {get; set;} = string.Empty;
    public string Content {get; set;} = string.Empty;
    public string Tags {get; set;} = string.Empty;
    public DateTime? ModifiedOn {get; set;}
    public virtual ICollection<PostRating> PostRatings {get; set;}
    public virtual ICollection<PostView> PostViews {get; set;}
    public string AuthorId {get; set;} = string.Empty;
    public virtual ICollection<Comment> Comments {get; set;}
    public virtual User Author {get; set;} = new User(); 
}