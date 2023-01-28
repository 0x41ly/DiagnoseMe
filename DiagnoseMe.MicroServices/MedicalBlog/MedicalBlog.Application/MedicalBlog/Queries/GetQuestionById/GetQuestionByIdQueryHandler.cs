using ErrorOr;
using MapsterMapper;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;
using MedicalBlog.Application.MedicalBlog.Queries.GetAnswersByQuestionId;
using MedicalBlog.Domain.Common.Errors;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetQuestion;

public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, ErrorOr<QuestionResponse>>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public GetQuestionByIdQueryHandler(
        IQuestionRepository questionRepository, 
        IMapper mapper,
        ISender mediator)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<ErrorOr<QuestionResponse>> Handle(GetQuestionByIdQuery query, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetByIdAsync(query.QuestionId);
        if (question == null)
        {
            return Errors.Question.NotFound;
        }
        var answersResponses = _mediator.Send(new GetAnswersByQuestionIdQuery(
            query.QuestionId,
            1)).Result.Value;
        var questionResponse = new QuestionResponse(
            question.Id!,
            question.QuestionString,
            _mapper.Map<UserData>(question.AskingUser),
            question.CreatedOn.ToString(),
            question.ModifiedOn?.ToString(),
            answersResponses,
            question.Answers.Count
        );
        return questionResponse;
    }
}