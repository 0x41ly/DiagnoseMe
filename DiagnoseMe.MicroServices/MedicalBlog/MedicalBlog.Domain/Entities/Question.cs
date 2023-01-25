namespace MedicalBlog.Domain.Entities;

public class Question : BaseEntity{

    public string QuestionString {get; set;} = string.Empty;
    public virtual ICollection<Answer> Answers {get; set;} = new HashSet<Answer>();
    

}