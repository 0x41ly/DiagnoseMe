using ErrorOr;
using MapsterMapper;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetQuestions;

public class GetQuestionsQueryHandler : IRequestHandler<GetQuestionsQuery, ErrorOr<List<QuestionResponse>>>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IMapper _mapper;

    public GetQuestionsQueryHandler(
        IQuestionRepository questionRepository,
        IMapper mapper)
    {
        _questionRepository = questionRepository;

        _mapper = mapper;
    }
 
    public async Task<ErrorOr<List<QuestionResponse>>> Handle(GetQuestionsQuery query, CancellationToken cancellationToken)
    {
        var questions = (await _questionRepository
            .GetAllAsync())
            .OrderByDescending(x => x.CreatedOn)
            .Skip((query.PageNumber - 1) * 10)
            .ToList();
        var questionsResponse = new List<QuestionResponse>();
        foreach (var question in questions)
        {
            var askingUser = _mapper.Map<UserData>(question.AskingUser);
            var questionsAnswersCount = question.Answers.Count;
            questionsResponse.Add(new QuestionResponse(
                question.Id!,
                question.QuestionString,
                askingUser,
                question.CreatedOn.ToString(),
                question.ModifiedOn?.ToString(),
                null,
                questionsAnswersCount
            ));
        }
        return questionsResponse;
    }
}