using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Commands.EditAnswer;

public class EditAnswerCommandValidator : AbstractValidator<EditAnswerCommand>
{
    public EditAnswerCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User id is required.");
        RuleFor(x => x.AnswerId)
            .NotEmpty()
            .WithMessage("Answer id is required.");
        RuleFor(x => x.Answerstring)
            .NotEmpty()
            .WithMessage("Answer is required.");
    }
}