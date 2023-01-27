using ErrorOr;
using MapsterMapper;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetQuestions;

public class GetQuestionsQueryHandler : IRequestHandler<GetQuestionsQuery, ErrorOr<List<QuestionResponse>>>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IAnswerRepository _answerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetQuestionsQueryHandler(
        IQuestionRepository questionRepository,
        IAnswerRepository answerRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _questionRepository = questionRepository;
        _answerRepository = answerRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<QuestionResponse>>> Handle(GetQuestionsQuery query, CancellationToken cancellationToken)
    {
        var questions = (await _questionRepository
            .GetAllAsync())
            .Skip((query.PageNumber - 1) * 10)
            .ToList();
        var askingUsersId = questions.Select(q => q.AskingUserId).ToList();
        var questionsId = questions.Select(q => q.Id).ToList();
        var answers = await _answerRepository.GetByQuestionsIdAsync(questionsId!);
        var answeringUsersId = answers.Select(a => a.AnsweringDoctorId).ToList();
        var allUsers = _mapper.Map<List<UserData>>(await _userRepository.GetAllAsync());
        var questionsResponse = new List<QuestionResponse>();
        foreach (var question in questions)
        {
            var askingUser = allUsers.FirstOrDefault(u => u.Id == question.AskingUserId)!;
            var questionsAnswers = answers.Where(a => a.QuestionId == question.Id).ToList();
            questionsResponse.Add(new QuestionResponse(
                question.Id!,
                question.QuestionString,
                askingUser,
                question.CreatedOn.ToString(),
                question.ModifiedOn?.ToString(),
                null,
                questionsAnswers.Count
            ));
        }
        return questionsResponse;
    }
}