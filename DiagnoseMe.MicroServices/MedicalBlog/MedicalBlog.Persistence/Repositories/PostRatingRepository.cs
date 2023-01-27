namespace MedicalBlog.Persistence.Repositories;
public class PostRatingRepository : BaseRepo<PostRating>, IPostRatingRepository
{
    public PostRatingRepository(DbContext db) : base(db)
    {
    }
    public async Task<List<PostRating>> GetByPostIdAsync(string postId)
    {
        return await table
            .Where(pss => pss.PostId == postId)
            .Include(x => x.User)
            .ToListAsync();
    }

    public async Task<List<PostRating>> GetByPostsIdAsync(List<string?> postsId)
    {
        return await table
            .Where(pss => postsId.Contains(pss.PostId!))
            .Include(x => x.User)
            .ToListAsync();
    }
}
