

namespace Core.Domain.Entities;

public class PostStateSuggestion : BaseEntity{
    public PostStateSuggestion()
    {
        Doctor = new();
        Post = new();
    }

    public string Type {get; set;}=string.Empty;
    public Guid DoctorId {get; set;} 
    public Guid PostId {get; set;}
    public Doctor Doctor {set; get;}
    public Post Post {set; get;}

}