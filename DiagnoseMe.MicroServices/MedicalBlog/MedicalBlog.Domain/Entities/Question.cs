namespace MedicalBlog.Domain.Entities;

public class Question : BaseEntity{

    public string AskingUserId {get; set;} = string.Empty;
    public string QuestionString {get; set;} = string.Empty;
    public DateTime? ModifiedOn {get; set;} = DateTime.UtcNow;
    public virtual ICollection<Answer> Answers {get; set;} = new HashSet<Answer>();
    public virtual User AskingUser {get; set;} = new User();
}