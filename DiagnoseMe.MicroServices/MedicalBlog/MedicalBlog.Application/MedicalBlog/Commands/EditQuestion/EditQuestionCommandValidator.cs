using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Commands.EditQuestion;

public class EditQuestionCommandValidator : AbstractValidator<EditQuestionCommand>
{
    public EditQuestionCommandValidator()
    {
        RuleFor(x => x.QuestionId)
            .NotEmpty()
            .WithMessage("QuestionId is required");
        RuleFor(x => x.QuestionString)
            .NotEmpty()
            .WithMessage("QuestionString is required");
        RuleFor(x => x.askingUserId)
            .NotEmpty()
            .WithMessage("askingUserId is required");
    }
}