
namespace Core.Domain.Entities;

public class Post : BaseEntity{
    public Post()
    {
        PostStateSuggestions = new HashSet<PostStateSuggestion>();
        Doctor = new();
        Comments = new HashSet<Comment>();
    }

    public string Content {get; set;} = string.Empty;
    public virtual ICollection<PostStateSuggestion> PostStateSuggestions {get; set;}
    public Guid DoctorId {get; set;} 
    public virtual Doctor Doctor {get; set;}
    public virtual ICollection<Comment> Comments {get; set;}
}