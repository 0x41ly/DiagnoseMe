using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetCommentsyParentId;

public record GetCommentsByParentIdQuery(
    string ParentId,
    int PageNumber) : IRequest<ErrorOr<List<CommentResponse>>>;