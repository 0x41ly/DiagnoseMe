using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Queries;

public class QueryHelper
{
    public static PostResponse MapPostResponse(
        Post post,
        int postRatingCount,
        int commentsCount,
        UserData authorData,
        List<UserData> ratingUsers,
        List<CommentResponse>? commentsResponse,
        int viewsCount,
        List<UserData> viewingUsers,
        int avgRating)
    {
        return new PostResponse(
                    post.Title,
                    post.Content,
                    authorData,
                    post.Tags.Split(',').ToList(),
                    post.CreationDate.ToString(),
                    post.ModifiedOn.ToString(),
                    commentsCount,
                    postRatingCount, 
                    ratingUsers,
                    commentsResponse,
                    viewsCount,
                    viewingUsers,
                    avgRating
                );
    }
}