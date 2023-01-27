namespace MedicalBlog.Persistence.Repositories;
public class QuestionRepository : BaseRepo<Question>, IQuestionRepository
{
    public QuestionRepository(DbContext db) : base(db)
    {
    }

    public async Task<List<Question>> GetByAskingUserIdAsync(string askingUserId)
    {
        return await table
            .Where(q => q.AskingUserId == askingUserId)
            .ToListAsync();
    }
}
