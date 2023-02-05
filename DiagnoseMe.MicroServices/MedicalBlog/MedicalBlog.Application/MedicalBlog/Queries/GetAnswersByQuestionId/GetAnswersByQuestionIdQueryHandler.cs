using ErrorOr;
using MapsterMapper;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetAnswersByQuestionId;

public class GetAnswersByQuestionIdQueryHandler : IRequestHandler<GetAnswersByQuestionIdQuery, ErrorOr<List<AnswerResponse>>>
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IMapper _mapper;

    public GetAnswersByQuestionIdQueryHandler(
        IAnswerRepository answerRepository, 
        IMapper mapper)
    {
        _answerRepository = answerRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<AnswerResponse>>> Handle(GetAnswersByQuestionIdQuery query, CancellationToken cancellationToken)
    {
        var answers = (await _answerRepository
            .GetByQuestionIdAsync(query.QuestionId))
            .OrderByDescending(x => x.CreatedOn)
            .Skip((query.PageNumber - 1) * 10)
            .Take(10)
            .ToList();
        var answersResponse = new List<AnswerResponse>();
        foreach (var answer in answers)
        {
            answersResponse.Add(new AnswerResponse(
                answer.Id!,
                answer.AnswerString,
                _mapper.Map<UserData>(answer.AnsweringDoctor),
                answer.CreatedOn.ToString(),
                answer.ModifiedOn?.ToString(),
                answer.AnswerAgreements.Count,
                _mapper.Map<List<UserData>>(answer.AnswerAgreements.Select(x => x.AnsweringUser).ToList())
            ));
        }
        return answersResponse;
    }
}