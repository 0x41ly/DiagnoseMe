namespace Core.Persistence.Repositories;
public class QuestionRepository : BaseRepo<Question>, IQuestionRepository
{
    public QuestionRepository(DbContext db) : base(db)
    {
    }
}
