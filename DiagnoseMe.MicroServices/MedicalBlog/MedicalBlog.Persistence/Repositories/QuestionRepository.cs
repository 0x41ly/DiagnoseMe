namespace MedicalBlog.Persistence.Repositories;
public class QuestionRepository : BaseRepo<Question>, IQuestionRepository
{
    public QuestionRepository(DbContext db) : base(db)
    {
    }
    public async override Task<List<Question>> GetAllAsync()
    {
        return await table
            .Include(q => q.AskingUser)
            .Include(q => q.Answers)
            .ThenInclude(a => a.AnsweringDoctor)
            .ToListAsync(); 
    }
    public async Task<List<Question>> GetByAskingUserIdAsync(string askingUserId)
    {
        return await table
            .Where(q => q.AskingUserId == askingUserId)
            .Include(q => q.AskingUser)
            .Include(q => q.Answers)
            .ThenInclude(a => a.AnsweringDoctor)
            .ToListAsync();
    }
}
