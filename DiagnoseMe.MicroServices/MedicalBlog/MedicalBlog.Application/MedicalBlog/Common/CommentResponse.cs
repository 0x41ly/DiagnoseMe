namespace MedicalBlog.Application.MedicalBlog.Common;

public record CommentResponse(
    string Id,
    string Content,
    UserData AuthorData,
    string CreatedOn,
    string? ModifiedOn,
    int ComentAgreementsCount,
    List<UserData> CommentAgreementUsers
);