namespace MedicalBlog.Domain.Entities;

public class Answer : BaseEntity{

    public string AnswerString {get; set;} = string.Empty;
    public string? UserId {get; set;} 
    public virtual string? QuestionId  {get; set;}
    public virtual Question Question  {get; set;} = new();
    public virtual ICollection<AnswerAgreement> AnswerAgreements {get; set;} = new HashSet<AnswerAgreement>();
    public virtual User User {get; set;} = new User();    
}