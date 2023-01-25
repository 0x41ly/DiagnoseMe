using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Queries;

public class QueryHelper
{
    public static PostResponse MapPostResponse(
        Post post,
        List<PostSuggestion> postSuggestion,
        List<Comment> comments,
        UserData authorData,
        List<UserData> suggestingUsers,
        List<CommentResponse>? commentsResponse)
    {
        return new PostResponse(
                    post.Title,
                    post.Content,
                    authorData,
                    post.Tags.Split(',').ToList(),
                    post.CreationDate.ToString(),
                    post.ModifiedOn.ToString(),
                    comments.Count,
                    postSuggestion.Count, 
                    suggestingUsers,
                    commentsResponse
                );
    }
}