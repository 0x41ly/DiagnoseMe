using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Commands.ReplyComment;

public record ReplyToCommentCommand(
    string PostId,
    string ParentId,
    string AuthorId,
    string Content) : IRequest<ErrorOr<CommandResponse>>;
