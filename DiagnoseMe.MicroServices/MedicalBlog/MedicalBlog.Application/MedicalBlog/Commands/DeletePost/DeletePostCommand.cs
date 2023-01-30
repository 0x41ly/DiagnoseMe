using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Commands.DeletePost;

public record DeletePostCommand(
    string PostId,
    string UserId) : IRequest<ErrorOr<CommandResponse>>;