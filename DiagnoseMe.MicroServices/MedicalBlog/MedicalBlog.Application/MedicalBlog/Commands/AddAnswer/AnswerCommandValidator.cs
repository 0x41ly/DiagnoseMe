using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Commands.AddAnswer;

public class AnswerCommandValidator : AbstractValidator<AnswerCommand>
{
    public AnswerCommandValidator()
    {
        RuleFor(x => x.AnsweringDoctorId)
            .NotEmpty()
            .WithMessage("User id is required.");
        RuleFor(x => x.QuestionId)
            .NotEmpty()
            .WithMessage("Question id is required.");
        RuleFor(x => x.AnswerString)
            .NotEmpty()
            .WithMessage("Answer is required.");
    }
}