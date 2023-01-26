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
            .ToListAsync();
    }

    public Task<List<PostRating>> GetByPostsIdAsync(List<string?> postsId)
    {
        return table
            .Where(pss => postsId.Contains(pss.PostId!))
            .ToListAsync();
    }
}
