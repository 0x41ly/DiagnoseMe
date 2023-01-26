namespace MedicalBlog.Persistence.Repositories;

public class PostViewRepository : BaseRepo<PostView>, IPostViewRepository
{
    public PostViewRepository(DbContext db) : base(db)
    {
    }  
     public async Task<List<PostView>> GetByPostIdAsync(string postId)
    {
        return await table
            .Where(pss => pss.PostId == postId)
            .ToListAsync();
    }

    public Task<List<PostView>> GetByPostsIdAsync(List<string?> postsId)
    {
        return table
            .Where(pss => postsId.Contains(pss.PostId!))
            .ToListAsync();
    } 
}