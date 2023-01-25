namespace MedicalBlog.Domain.Entities;

public class PostSuggestion : BaseEntity{
    public PostSuggestion()
    {
        Post = new();
    }

    public string Type {get; set;}=string.Empty;
    public string? UserId {get; set;} 
    public string? PostId {get; set;}
    public Post Post {set; get;}

}