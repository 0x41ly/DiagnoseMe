using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Commands.AgreeComment;

public record AgreeCommentCommand(
    string CommentId,
    string UserId) : IRequest<ErrorOr<CommandResponse>>;