using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetQuestion;

public class GetQuestionByIdQueryValidator : AbstractValidator<GetQuestionByIdQuery>
{
    public GetQuestionByIdQueryValidator()
    {
        RuleFor(x => x.QuestionId)
            .NotEmpty()
            .WithMessage("QuestionId is required");
    }
}