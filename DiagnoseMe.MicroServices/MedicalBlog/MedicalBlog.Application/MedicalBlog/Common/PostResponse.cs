namespace MedicalBlog.Application.MedicalBlog.Common;

public record PostResponse(
    string Title,
    string Content,
    UserData AuthorData,
    List<string> Tags,
    string CreatedOn,
    string? ModifiedOn,
    int CommentsCount,
    int SugguestionsCount,
    List<UserData> SuggestingUsers,
    List<CommentResponse>? Comments
);