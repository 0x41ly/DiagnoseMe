using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Commands.DeleteAnswer;

public record DeleteAnswerCommand(
    string UserId,
    string AnswerId,
    List<string> Roles) : IRequest<ErrorOr<CommandResponse>>;