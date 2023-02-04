using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Commands.DeleteComment;

public record DeleteCommentCommand(
    string CommentId,
    string UserId,
    List<string> Roles) : IRequest<ErrorOr<CommandResponse>>;
