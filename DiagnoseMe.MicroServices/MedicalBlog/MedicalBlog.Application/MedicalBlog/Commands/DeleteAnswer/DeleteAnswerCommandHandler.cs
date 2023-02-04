using ErrorOr;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;
using MedicalBlog.Domain.Common.Errors;
using MedicalBlog.Domain.Common.Roles;

namespace MedicalBlog.Application.MedicalBlog.Commands.DeleteAnswer;


public class DeleteAnswerCommandHandler : IRequestHandler<DeleteAnswerCommand, ErrorOr<CommandResponse>>
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IUserRepository _userRepository;
    public DeleteAnswerCommandHandler(
        IAnswerRepository answerRepository,
        IUserRepository userRepository)
    {
        _answerRepository = answerRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<CommandResponse>> Handle(DeleteAnswerCommand command, CancellationToken cancellationToken)
    {
        var answer = await _answerRepository.GetByIdAsync(command.AnswerId);
        if (answer is null)
            return Errors.Answer.NotFound;
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user is null){
            // TODO: Check user in auth service
            return Errors.User.NotFound;
        }
        if (answer.AnsweringDoctorId != command.UserId || !command.Roles.Contains(Roles.Admin))
            return Errors.User.YouCanNotDoThis;
        try
        {
            _answerRepository.Remove(answer);
            await _answerRepository.Save();
        }
        catch (Exception)
        {
            return Errors.Answer.DeletionFailed;
        }
        return new CommandResponse(
            true,
            $"Answer with id: {answer.Id} was deleted.",
            $"/api/questions/{answer.QuestionId}"
        );
    }
}