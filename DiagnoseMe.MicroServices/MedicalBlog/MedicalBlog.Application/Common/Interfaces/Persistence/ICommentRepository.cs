
namespace MedicalBlog.Application.Common.Interfaces.Persistence;
public interface ICommentRepository : IBaseRepo<Comment>
{
    Task<List<Comment>> GetByPostIdAsync(string postId);
    Task<List<Comment>> GetByParentIdAsync(string parentId);
}
