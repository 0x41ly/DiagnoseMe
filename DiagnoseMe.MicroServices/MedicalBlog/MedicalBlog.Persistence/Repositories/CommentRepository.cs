namespace MedicalBlog.Persistence.Repositories;
public class CommentRepository : BaseRepo<Comment>, ICommentRepository
{
    public CommentRepository(DbContext db) : base(db)
    {
    }

    public async Task<List<Comment>> GetByParentIdAsync(string parentId)
    {
        return await table
            .Where(c => c.ParentId == parentId)
            .Include(c => c.Author)
            .Include(c => c.CommentAgreements)
            .ThenInclude(ca => ca.User)
            .ToListAsync();
    }

    public async Task<List<Comment>> GetByPostIdAsync(string postId)
    {
        return await table
            .Where(c => c.PostId == postId)
            .Include(c => c.Author)
            .Include(c => c.CommentAgreements)
            .ThenInclude(ca => ca.User)
            .ToListAsync();
    }
}
