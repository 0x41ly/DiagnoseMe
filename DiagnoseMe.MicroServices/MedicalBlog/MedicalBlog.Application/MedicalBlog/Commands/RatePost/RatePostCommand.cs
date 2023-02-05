using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Commands.RatePost;

public record RatePostCommand(
    string PostId,
    int Rating,
    string UserId) : IRequest<ErrorOr<CommandResponse>>;