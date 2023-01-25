namespace MedicalBlog.Domain.Entities;

public class CommentAgreement : BaseEntity{

    public string? DoctorId {get;set;}
    public string? CommentId {get;set;}
    public bool IsAgreed { get; set; } 
    public virtual Comment Comment {get; set;} = new();

}