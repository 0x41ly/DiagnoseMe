namespace MedicalBlog.Domain.Entities;

public class Comment : BaseEntity{

    public Comment(){
        ChildComments = new HashSet<Comment>();
        CommentAgreements= new HashSet<CommentAgreement>();
    }

    public string  ParentId {get; set;} = string.Empty;
    public string AuthorId {get; set;} = string.Empty;
    public string PostId {get; set;} = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime? ModifiedOn {get; set;}
    public virtual User Author {get; set;} = new User();
    public virtual Comment? ParentComment {get;set;}
    public virtual Post Post {get;set;} = new Post();
    public virtual ICollection<Comment> ChildComments {get; set;} 
    public virtual ICollection<CommentAgreement> CommentAgreements {get; set;}
}