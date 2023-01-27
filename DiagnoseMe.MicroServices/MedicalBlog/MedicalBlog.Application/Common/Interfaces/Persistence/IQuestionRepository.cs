
namespace MedicalBlog.Application.Common.Interfaces.Persistence;
public interface IQuestionRepository : IBaseRepo<Question>
{
    Task<List<Question>> GetByAskingUserIdAsync(string askingUserId);
}
