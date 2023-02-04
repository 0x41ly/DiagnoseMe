using ErrorOr;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;
using MedicalBlog.Domain.Common.Errors;

namespace MedicalBlog.Application.MedicalBlog.Commands.AddAnswer;

public class AnswerCommandHandler : IRequestHandler<AnswerCommand, ErrorOr<CommandResponse>>
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IUserRepository _userRepository;

    public AnswerCommandHandler(
        IAnswerRepository answerRepository,
        IQuestionRepository questionRepository,
        IUserRepository userRepository)
    {
        _answerRepository = answerRepository;
        _questionRepository = questionRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<CommandResponse>> Handle(AnswerCommand command, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetByIdAsync(command.QuestionId);
        if (question is null)
            return Errors.Question.NotFound;
        var user = await _userRepository.GetByIdAsync(command.AnsweringDoctorId);
        if (user is null){
            // TODO: Check user in auth service
            return Errors.User.NotFound;
        }
        var answer = new Answer
        {
            Id = Guid.NewGuid().ToString(),
            AnswerString = command.AnswerString,
            AnsweringDoctorId = command.AnsweringDoctorId,
            QuestionId = command.QuestionId
        };
        try
        {
            await _answerRepository.AddAsync(answer);
            await _answerRepository.Save();
        }
        catch (Exception)
        {
            return Errors.Answer.AddFailed;
        }
        return new CommandResponse(
            true,
            $"Answer with id: {answer.Id} was added.",
            $"/api/questions/{question.Id}"
        );
    }
}