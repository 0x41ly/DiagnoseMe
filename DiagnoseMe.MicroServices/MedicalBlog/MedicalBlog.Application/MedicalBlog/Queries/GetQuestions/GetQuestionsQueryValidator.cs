using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetQuestions;

public class GetQuestionsQueryValidator : AbstractValidator<GetQuestionsQuery>
{
    public GetQuestionsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .NotEmpty()
            .WithMessage("PageNumber cannot be empty.")
            .GreaterThan(0)
            .WithMessage("PageNumber must be positive");
    }
}