namespace MedicalBlog.Persistence.Repositories;
public class CommentAgreementRepository : BaseRepo<CommentAgreement>, ICommentAgreementRepository
{
    public CommentAgreementRepository(DbContext db) : base(db)
    {
    }

    public async Task<List<CommentAgreement>> GetCommentAgreementsByCommentsIdAsync(List<string> commentsId)
    {
        return await table
            .Where(x => commentsId.Contains(x.CommentId!))
            .Include(x => x.User)
            .ToListAsync();
    }
}
