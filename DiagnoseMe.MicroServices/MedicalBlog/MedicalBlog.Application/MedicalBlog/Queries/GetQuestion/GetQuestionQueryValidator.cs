using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetQuestion;

public class GetQuestionQueryValidator : AbstractValidator<GetQuestionQuery>
{
    public GetQuestionQueryValidator()
    {
        RuleFor(x => x.QuestionId)
            .NotEmpty()
            .WithMessage("QuestionId is required");
    }
}