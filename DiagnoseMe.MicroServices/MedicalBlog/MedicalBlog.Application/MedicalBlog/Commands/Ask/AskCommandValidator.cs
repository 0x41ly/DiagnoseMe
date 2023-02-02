using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Commands.Ask;

public class AskCommandValidator : AbstractValidator<AskCommand>
{
    public AskCommandValidator()
    {
        RuleFor(x => x.AskingUserId)
            .NotEmpty()
            .WithMessage("AskingUserId is required.");

        RuleFor(x => x.QuestionString)
            .NotEmpty()
            .WithMessage("QuestionString is required.");


    }
}