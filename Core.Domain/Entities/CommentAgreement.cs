namespace Core.Domain.Entities;

public class CommentAgreement : BaseEntity{

    public Guid DoctorId {get;set;}
    public Guid CommentId {get;set;}
    public bool IsAgreed { get; set; } 

    public virtual Doctor Doctor {get; set; } = new Doctor();
    public virtual Comment Comment {get; set;} = new();

}