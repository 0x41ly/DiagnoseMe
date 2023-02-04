using ErrorOr;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;
using MedicalBlog.Domain.Common.Errors;
using MedicalBlog.Domain.Common.Roles;

namespace MedicalBlog.Application.MedicalBlog.Commands.DeleteQuestion;

public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, ErrorOr<CommandResponse>>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IUserRepository _userRepository;

    public DeleteQuestionCommandHandler(
        IQuestionRepository questionRepository, 
        IUserRepository userRepository)
    {
        _questionRepository = questionRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<CommandResponse>> Handle(DeleteQuestionCommand command, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetByIdAsync(command.QuestionId);

        if (question is null)
            return Errors.Question.NotFound;
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user is null){
            // TODO: Check user in auth service
            return Errors.User.NotFound;
        }
        if (question.AskingUser.Id != user.Id || !command.Roles.Contains(Roles.Admin))
            return Errors.User.YouCanNotDoThis;

        try{
            _questionRepository.Remove(question);
            await _questionRepository.Save();
        }
        catch (Exception)
        {
            return Errors.Question.DeletionFailed;
        }

        return new CommandResponse(
            true,
            $"Question with id: {question.Id} was deleted.",
            "/api/questions"
        );
    }
}