using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Commands.EditPost;

public record EditPostCommand(
    string PostId,
    string Title,
    string Content,
    List<string> Tags,
    string UserId) : IRequest<ErrorOr<CommandResponse>>;