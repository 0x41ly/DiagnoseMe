namespace MedicalBlog.Application.Common.Interfaces.Persistence;

public interface IPostViewRepository : IBaseRepo<PostView>
{
    Task<List<PostView>> GetByPostIdAsync(string postId);
    Task<List<PostView>> GetByPostsIdAsync(List<string?> postsId);
}