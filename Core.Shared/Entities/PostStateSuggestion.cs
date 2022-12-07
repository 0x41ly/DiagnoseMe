

namespace Core.Shared.Entities;

public class PostSateSuggestion : BaseEntity{
    public PostSateSuggestion()
    {
        User = new();
        Post = new();
    }

    public string Type {get; set;}=string.Empty;
    public string UserId {get; set;} = string.Empty;
    public Guid PostId {get; set;}
    public ApplicationUser User {set; get;}
    public Post Post {set; get;}

}