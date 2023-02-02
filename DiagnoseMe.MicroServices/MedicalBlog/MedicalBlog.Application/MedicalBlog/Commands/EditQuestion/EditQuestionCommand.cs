using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Commands.EditQuestion;

public record EditQuestionCommand(
    string QuestionId,
    string QuestionString,
    string askingUserId
) : IRequest<ErrorOr<CommandResponse>>;