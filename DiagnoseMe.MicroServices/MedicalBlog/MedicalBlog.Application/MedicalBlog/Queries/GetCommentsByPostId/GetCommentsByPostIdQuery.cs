using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;
namespace MedicalBlog.Application.MedicalBlog.Queries.GetCommentsByPostId;

public record GetCommentsByPostIdQuery(
    string PostId,
    int PageNumber) : IRequest<ErrorOr<List<CommentResponse>>>;