using System.Security.Policy;

namespace Core.Shared.Entities;

public class Post : BaseEntity{
    public Post()
    {
        PostSateSuggestions = new HashSet<PostSateSuggestion>();
        User = new();
        Comments = new HashSet<Comment>();
    }

    public string Content {get; set;} = string.Empty;
    public virtual ICollection<PostSateSuggestion> PostSateSuggestions {get; set;}
    public string UserId {get; set;} = string.Empty;
    public virtual ApplicationUser User {get; set;}
    public virtual ICollection<Comment> Comments {get; set;}
}