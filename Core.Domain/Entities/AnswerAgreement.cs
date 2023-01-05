namespace Core.Domain.Entities;

public class AnswerAgreement : BaseEntity{

    public bool IsAgreed {get; set;}
    public Guid DoctorId {get; set;}
    public Guid AnswerId {get; set;}
    public virtual Doctor Doctor  {get; set;} = new();
    public virtual Answer Answer  {get; set;} = new();


}