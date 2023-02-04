using ErrorOr;
using MapsterMapper;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;
using MedicalBlog.Domain.Common.Errors;

namespace MedicalBlog.Application.MedicalBlog.Commands.Ask;

public class AskCommandHandler : IRequestHandler<AskCommand, ErrorOr<CommandResponse>>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IUserRepository _userRepository;

    public AskCommandHandler(
        IQuestionRepository questionRepository,
        IUserRepository userRepository)
    {
        _questionRepository = questionRepository;
        _userRepository = userRepository;
    }
    public async Task<ErrorOr<CommandResponse>> Handle(AskCommand command, CancellationToken cancellationToken)
    {
        
        var askingUser = await _userRepository.GetByIdAsync(command.AskingUserId);
        if (askingUser == null)
        {
            // TODO: get the user from the auth service and add it to the db
            return Errors.User.SomethingWentWrong;
        }
        var question = new Question
        {
            Id = Guid.NewGuid().ToString(),
            QuestionString = command.QuestionString,
            AskingUserId = command.AskingUserId,
            CreatedOn = DateTime.UtcNow
        };
        try
        {
            await _questionRepository.AddAsync(question);
            await _questionRepository.Save();
        }
        catch
        {
            return Errors.Question.CreationFailed;
        }
        return new CommandResponse(
            true,
            "Question created successfully",
            $"/api/questions/{question.Id}");
    }
}