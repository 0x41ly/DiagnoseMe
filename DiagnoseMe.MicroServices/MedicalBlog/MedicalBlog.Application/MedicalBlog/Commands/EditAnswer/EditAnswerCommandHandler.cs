using ErrorOr;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;
using MedicalBlog.Domain.Common.Errors;

namespace MedicalBlog.Application.MedicalBlog.Commands.EditAnswer;

public class EditAnswerCommandHandler : IRequestHandler<EditAnswerCommand, ErrorOr<CommandResponse>>
{
   private readonly IAnswerRepository _answerRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IUserRepository _userRepository;
    public EditAnswerCommandHandler(
        IAnswerRepository answerRepository,
        IQuestionRepository questionRepository,
        IUserRepository userRepository)
    {
        _answerRepository = answerRepository;
        _questionRepository = questionRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<CommandResponse>> Handle(EditAnswerCommand command, CancellationToken cancellationToken)
    {
        var answer = await _answerRepository.GetByIdAsync(command.AnswerId);
        if (answer is null)
            return Errors.Answer.NotFound;
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user is null){
            // TODO: Check user in auth service
            return Errors.User.NotFound;
        }
        if (answer.AnsweringDoctorId != command.UserId)
            return Errors.User.YouCanNotDoThis;
        var question = await _questionRepository.GetByIdAsync(answer.QuestionId);
        if (question is null)
            return Errors.Question.NotFound;
        answer.AnswerString = command.Answerstring;
        answer.ModifiedOn = DateTime.UtcNow;
        try
        {
            await _answerRepository.Edit(answer);
            await _answerRepository.Save();
        }
        catch (Exception)
        {
            return Errors.Answer.EditFailed;
        }
        return new CommandResponse(
            true,
            $"Answer with id: {answer.Id} was edited.",
            $"/api/questions/{answer.QuestionId}"
        );
    }
}