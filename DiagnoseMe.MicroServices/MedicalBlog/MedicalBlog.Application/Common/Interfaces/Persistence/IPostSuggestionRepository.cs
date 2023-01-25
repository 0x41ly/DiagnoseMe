
namespace MedicalBlog.Application.Common.Interfaces.Persistence;
public interface IPostSuggestionRepository : IBaseRepo<PostSuggestion>
{
    Task<List<PostSuggestion>> GetByPostIdAsync(string postId);
    Task<List<PostSuggestion>> GetByPostsIdAsync(List<string?> postsId);
}
