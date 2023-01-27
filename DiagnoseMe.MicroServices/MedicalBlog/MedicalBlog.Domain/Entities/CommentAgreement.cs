namespace MedicalBlog.Domain.Entities;

public class CommentAgreement : BaseEntity{

    public string UserId {get;set;} = string.Empty;
    public string CommentId {get;set;} = string.Empty;
    public bool IsAgreed { get; set; } 
    public virtual Comment Comment {get; set;} = new();
    public virtual User User {get; set;} = new();
}