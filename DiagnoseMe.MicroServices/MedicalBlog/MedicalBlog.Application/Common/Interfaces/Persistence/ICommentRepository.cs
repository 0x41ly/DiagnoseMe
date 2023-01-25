
namespace MedicalBlog.Application.Common.Interfaces.Persistence;
public interface ICommentRepository : IBaseRepo<Comment>
{
    Task<List<Comment>> GetCommentsByPostIdAsync(string postId);
}
