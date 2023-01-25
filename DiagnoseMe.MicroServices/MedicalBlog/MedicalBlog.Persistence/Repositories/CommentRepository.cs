namespace MedicalBlog.Persistence.Repositories;
public class CommentRepository : BaseRepo<Comment>, ICommentRepository
{
    public CommentRepository(DbContext db) : base(db)
    {
    }
    public async Task<List<Comment>> GetCommentsByPostIdAsync(string postId)
    {
        return await table
            .Where(c => c.PostId == postId)
            .ToListAsync();
    }
}
