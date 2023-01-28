using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetAnswersByQuestionId;

public class GetAnswersByQuestionIdQueryValidator : AbstractValidator<GetAnswersByQuestionIdQuery>
{
    public GetAnswersByQuestionIdQueryValidator()
    {
        RuleFor(x => x.QuestionId)
            .NotEmpty()
            .WithMessage("QuestionId is required");

        RuleFor(x => x.PageNumber)
            .NotEmpty()
            .WithMessage("PageNumber is required")
            .GreaterThan(0)
            .WithMessage("PageNumber must be positive");
    }
}