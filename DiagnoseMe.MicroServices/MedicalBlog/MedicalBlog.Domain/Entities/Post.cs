namespace MedicalBlog.Domain.Entities;

public class Post : BaseEntity{
    public Post()
    {
        PostSuggestion = new HashSet<PostSuggestion>();
        Comments = new HashSet<Comment>();
    }
    public string Title {get; set;} = string.Empty;
    public string Content {get; set;} = string.Empty;
    public string Tags {get; set;} = string.Empty;
    public DateTime? ModifiedOn {get; set;}
    public virtual ICollection<PostSuggestion> PostSuggestion {get; set;}
    public string? AuthorId {get; set;} 
    public virtual ICollection<Comment> Comments {get; set;}
}