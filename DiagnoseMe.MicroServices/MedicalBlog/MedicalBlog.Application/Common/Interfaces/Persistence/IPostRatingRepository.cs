
namespace MedicalBlog.Application.Common.Interfaces.Persistence;
public interface IPostRatingRepository : IBaseRepo<PostRating>
{
    Task<List<PostRating>> GetByPostIdAsync(string postId);
    Task<List<PostRating>> GetByPostsIdAsync(List<string?> postsId);
}
