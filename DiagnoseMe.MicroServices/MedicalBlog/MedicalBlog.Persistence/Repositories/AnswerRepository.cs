namespace MedicalBlog.Persistence.Repositories;
public class AnswerRepository : BaseRepo<Answer>, IAnswerRepository
{
    public AnswerRepository(DbContext db) : base(db)
    {
    }

    public async Task<List<Answer>> GetByQuestionsIdAsync(List<string> questionsId)
    {
        return await table
            .Where(a => questionsId.Contains(a.QuestionId))
            .Include(a => a.AnsweringDoctor)
            .ToListAsync();
    }
}
