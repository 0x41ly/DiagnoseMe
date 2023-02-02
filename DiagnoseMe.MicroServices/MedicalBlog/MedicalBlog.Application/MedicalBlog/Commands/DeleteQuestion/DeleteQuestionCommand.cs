using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Commands.DeleteQuestion;

public record DeleteQuestionCommand(
    string QuestionId,
    string UserId,
    List<string> Roles
) : IRequest<ErrorOr<CommandResponse>>;