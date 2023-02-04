using FluentValidation;
using MedicalBlog.Domain.Common.Roles;

namespace MedicalBlog.Application.MedicalBlog.Commands.DeleteQuestion;

public class DeleteQuestionCommandValidator : AbstractValidator<DeleteQuestionCommand>
{
    public DeleteQuestionCommandValidator()
    {
        RuleFor(command => command.QuestionId)
            .NotEmpty()
            .WithMessage("QuestionId is required");
        RuleFor(command => command.UserId)
            .NotEmpty()
            .WithMessage("UserId is required")
            .NotNull()
            .WithMessage("UserId is required");
        RuleFor(command => command.Roles)
            .NotEmpty()
            .WithMessage("Roles is required")
            .NotNull()
            .WithMessage("Roles is required")
            .Must(x => x.Contains(Roles.Admin) || x.Contains(Roles.Doctor))
            .WithMessage("User must be admin or doctor");
    }
}