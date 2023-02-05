using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Commands.DeleteCommentAgreement;

public record DeleteCommentAgreementCommand(
    string CommentId,
    string UserId) : IRequest<ErrorOr<CommandResponse>>;