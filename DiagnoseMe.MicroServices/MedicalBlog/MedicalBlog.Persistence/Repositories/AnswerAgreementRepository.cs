
namespace MedicalBlog.Persistence.Repositories;
public class AnswerAgreementRepository : BaseRepo<AnswerAgreement>, IAnswerAgreementRepository
{
    public AnswerAgreementRepository(DbContext db) : base(db)
    {
    }
}
