
namespace MedicalBlog.Application.Common.Interfaces.Persistence;
public interface IAnswerRepository : IBaseRepo<Answer>
{
    Task<List<Answer>> GetByQuestionsIdAsync(List<string> questionsId);
    Task<List<Answer>> GetByQuestionIdAsync(string questionId);
}
