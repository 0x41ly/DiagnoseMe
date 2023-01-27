namespace MedicalBlog.Application.MedicalBlog.Common;

public record CommentResponse(
    string Id,
    string ParentId,
    string Content,
    UserData AuthorData,
    string CreatedOn,
    string? ModifiedOn,
    int ComentAgreementsCount,
    List<UserData> CommentAgreementUsers
);