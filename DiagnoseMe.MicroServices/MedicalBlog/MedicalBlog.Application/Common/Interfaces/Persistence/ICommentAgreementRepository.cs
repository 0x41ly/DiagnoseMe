using System.Threading.Tasks;

namespace MedicalBlog.Application.Common.Interfaces.Persistence;
public interface ICommentAgreementRepository : IBaseRepo<CommentAgreement>
{
    Task<List<CommentAgreement>> GetCommentAgreementsByCommentsIdAsync(List<string> commentsId);
}
