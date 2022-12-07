using Microsoft.Extensions.Hosting;

namespace Core.Shared.Entities;

public class Comment : BaseEntity{

    public Comment(){
        ChildComments = new HashSet<Comment>();
        CommentAgreements= new HashSet<CommentAgreement>();
    }

    public Guid? ParentId {get; set;}
    public Guid OwnerId {get; set;}
    public Guid PostId {get; set;}
    public string Content { get; set; } = string.Empty;

    public virtual Comment? ParentComment {get;set;}
    public virtual Doctor Owner {get;set;} = new Doctor();
    public virtual Post Post {get;set;} = new Post();
    public virtual ICollection<Comment> ChildComments {get; set;} 
    public virtual ICollection<CommentAgreement> CommentAgreements {get; set;}



}