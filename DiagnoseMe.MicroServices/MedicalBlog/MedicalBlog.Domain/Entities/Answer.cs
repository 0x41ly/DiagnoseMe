namespace MedicalBlog.Domain.Entities;

public class Answer : BaseEntity
{ 
    public string AnswerString {get; set;} = string.Empty;
    public string AnsweringDoctorId {get; set;}  = string.Empty;
    public DateTime? ModifiedOn {get; set;}
    public  string QuestionId  {get; set;} = string.Empty;
    public virtual Question Question  {get; set;} = new();
    public virtual ICollection<AnswerAgreement> AnswerAgreements {get; set;} = new HashSet<AnswerAgreement>();
    public virtual User AnsweringDoctor {get; set;} = new User();    
} 