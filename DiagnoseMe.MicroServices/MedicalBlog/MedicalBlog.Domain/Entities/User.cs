using System;
namespace MedicalBlog.Domain.Entities;


public class User : BaseEntity
{
    public string Name {get; set;} = string.Empty;
    public string FullName {get; set;} = string.Empty;
    public string ProfilePictureUrl {get; set;} = string.Empty;
    public virtual ICollection<Answer> Answers {get; set;} = new HashSet<Answer>();
    public virtual ICollection<Comment> Comments {get; set;} = new HashSet<Comment>();
    public virtual ICollection<CommentAgreement> CommentAgreements {get; set;} = new HashSet<CommentAgreement>();
    public virtual ICollection<AnswerAgreement> AnswerAgreements {get; set;} = new HashSet<AnswerAgreement>();
    public virtual ICollection<PostRating> PostRatings {get; set;} = new HashSet<PostRating>();
    public virtual ICollection<Post> Posts {get; set;} = new HashSet<Post>();
    public virtual ICollection<PostView> PostViews {get; set;} = new HashSet<PostView>();
    public virtual ICollection<Question> Questions {get; set;} = new HashSet<Question>();
    public virtual ICollection<UserSubscribedUser> SubscribedUsers {get; set;} = new HashSet<UserSubscribedUser>();
    public virtual ICollection<UserSubscribedUser> Subscribers {get; set;} = new HashSet<UserSubscribedUser>();
}