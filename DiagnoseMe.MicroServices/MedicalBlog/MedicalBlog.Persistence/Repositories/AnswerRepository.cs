namespace MedicalBlog.Persistence.Repositories;
public class AnswerRepository : BaseRepo<Answer>, IAnswerRepository
{
    public AnswerRepository(DbContext db) : base(db)
    {
    }

    public async Task<List<Answer>> GetByQuestionIdAsync(string questionId)
    {
        return await table
            .Where(a => a.QuestionId == questionId)
            .Include(a => a.AnsweringDoctor)
            .Include(a => a.AnswerAgreements)
            .ThenInclude(a => a.AnsweringUser)
            .ToListAsync();
    }

    public async Task<List<Answer>> GetByQuestionsIdAsync(List<string> questionsId)
    {
        return await table
            .Where(a => questionsId.Contains(a.QuestionId))
            .Include(a => a.AnsweringDoctor)
            .ToListAsync();
    }
}
