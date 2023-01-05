namespace Core.Persistence.Repositories;
public class CommentAgreementRepository : BaseRepo<CommentAgreement>, ICommentAgreementRepository
{
    public CommentAgreementRepository(DbContext db) : base(db)
    {
    }
}
