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
        double avgRating)
    {
        return new PostResponse(
                    post.Id!,
                    post.Title,
                    post.Content,
                    authorData,
                    post.Tags.Split(',').ToList(),
                    post.CreatedOn.ToString(),
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