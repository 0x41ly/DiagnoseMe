
namespace MedicalBlog.Application.Common.Interfaces.Persistence;
public interface IPostRepository : IBaseRepo<Post>
{
    Task<List<Post>> GetByTagsAsync(List<string> tags);
    Task<List<Post>> GetByDocterIdAsync(string authorId);
}
