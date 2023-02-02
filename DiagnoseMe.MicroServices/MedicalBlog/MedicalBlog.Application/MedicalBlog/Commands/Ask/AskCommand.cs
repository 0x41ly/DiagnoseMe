using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Commands.Ask;

public record AskCommand(
    string AskingUserId,
    string QuestionString): IRequest<ErrorOr<CommandResponse>>;