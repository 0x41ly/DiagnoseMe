using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Commands.EditAnswer;

public record EditAnswerCommand(
    string UserId,
    string AnswerId,
    string Answerstring) : IRequest<ErrorOr<CommandResponse>>;