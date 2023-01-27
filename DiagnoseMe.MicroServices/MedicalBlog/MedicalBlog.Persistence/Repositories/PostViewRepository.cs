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
            .Include(x => x.User)
            .ToListAsync();
    }

    public async Task<List<PostView>> GetByPostsIdAsync(List<string?> postsId)
    {
        return await table
            .Where(pss => postsId.Contains(pss.PostId!))
            .Include(x => x.User)
            .ToListAsync();
    } 
}