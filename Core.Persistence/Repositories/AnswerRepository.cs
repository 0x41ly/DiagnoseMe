namespace Core.Persistence.Repositories;
public class AnswerRepository : BaseRepo<Answer>, IAnswerRepository
{
    public AnswerRepository(DbContext db) : base(db)
    {
    }
}
