namespace Core.Domain.Entities;

public class Answer : BaseEntity{

    public string AnswerString {get; set;} = string.Empty;
    public Guid DoctorId {get; set;}
    public virtual Doctor Doctor  {get; set;} = new();
    public virtual Guid QuestionId  {get; set;} = new();
    public virtual Question Question  {get; set;} = new();
    public virtual ICollection<AnswerAgreement> AnswerAgreements {get; set;} = new HashSet<AnswerAgreement>();    

    

}