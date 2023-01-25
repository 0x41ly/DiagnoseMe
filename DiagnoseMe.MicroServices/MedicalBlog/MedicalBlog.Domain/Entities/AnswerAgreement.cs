namespace MedicalBlog.Domain.Entities;
public class AnswerAgreement : BaseEntity{

    public bool IsAgreed {get; set;}
    public string? DoctorId {get; set;}
    public string? AnswerId {get; set;}
    public virtual Answer Answer  {get; set;} = new();


}