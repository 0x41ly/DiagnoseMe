using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetQuestionsByAskingUserId;


public class GetQuestionsByAskingUserIdQueryValidator : AbstractValidator<GetQuestionsByAskingUserIdQuery>
{
    public GetQuestionsByAskingUserIdQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .NotEmpty()
            .WithMessage("Page number is required")
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0");
        RuleFor(x => x.AskingUserId)
            .NotEmpty()
            .WithMessage("Asking user id is required");
    }
}