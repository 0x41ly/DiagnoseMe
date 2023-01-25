namespace MedicalBlog.Application.MedicalBlog.Common;

public record CommentResponse(
    string Content,
    UserData AuthorData,
    string CreatedOn,
    string? ModifiedOn
);