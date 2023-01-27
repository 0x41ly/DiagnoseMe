namespace MedicalBlog.Application.MedicalBlog.Common;

public record PostResponse(
    string Id,
    string Title,
    string Content,
    UserData AuthorData,
    List<string> Tags,
    string CreatedOn,
    string? ModifiedOn,
    int CommentsCount,
    int RatingCount,
    List<UserData> RatingUsers,
    List<CommentResponse>? Comments,
    int ViewsCount,
    List<UserData> ViewingUsers,
    double AvgRating

);