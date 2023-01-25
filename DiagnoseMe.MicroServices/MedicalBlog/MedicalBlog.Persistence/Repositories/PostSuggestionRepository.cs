namespace MedicalBlog.Persistence.Repositories;
public class PostSuggestionRepository : BaseRepo<PostSuggestion>, IPostSuggestionRepository
{
    public PostSuggestionRepository(DbContext db) : base(db)
    {
    }
    public async Task<List<PostSuggestion>> GetByPostIdAsync(string postId)
    {
        return await table
            .Where(pss => pss.PostId == postId)
            .ToListAsync();
    }

    public Task<List<PostSuggestion>> GetByPostsIdAsync(List<string?> postsId)
    {
        return table
            .Where(pss => postsId.Contains(pss.PostId!))
            .ToListAsync();
    }
}
