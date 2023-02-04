using ErrorOr;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;
using MedicalBlog.Domain.Common.Errors;

namespace MedicalBlog.Application.MedicalBlog.Commands.EditQuestion;


public class EditQuestionCommandHandler : IRequestHandler<EditQuestionCommand, ErrorOr<CommandResponse>>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IUserRepository _userRepository;

    public EditQuestionCommandHandler(
        IQuestionRepository questionRepository, 
        IUserRepository userRepository)
    {
        _questionRepository = questionRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<CommandResponse>> Handle(EditQuestionCommand command, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetByIdAsync(command.QuestionId);

        if (question is null)
            return Errors.Question.NotFound;
        var user = await _userRepository.GetByIdAsync(command.askingUserId);
        if (user is null){
            // TODO: Check user in auth service
            return Errors.User.NotFound;
        }
        if (question.AskingUser.Id != user.Id)
            return Errors.User.YouCanNotDoThis;

        try{
            question.QuestionString = command.QuestionString;
            question.ModifiedOn = DateTime.UtcNow;
            await _questionRepository.Edit(question);
            await _questionRepository.Save();
        }
        catch (Exception)
        {
            return Errors.Question.EditFailed;
        }

        return new CommandResponse(
            true,
            $"Question with id: {question.Id} was edited.",
            "/api/questions/{question.Id}"
        );
    }
}