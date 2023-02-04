using FluentValidation;
using MedicalBlog.Domain.Common.Roles;

namespace MedicalBlog.Application.MedicalBlog.Commands.DeleteAnswer;


public class DeleteAnswerCommandValidator : AbstractValidator<DeleteAnswerCommand>
{
    public DeleteAnswerCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User id is required.");
        RuleFor(x => x.AnswerId)
            .NotEmpty()
            .WithMessage("Answer id is required.");
        RuleFor(x => x.Roles)
            .NotEmpty()
            .WithMessage("Roles are required.")
            .Must(x => x.Contains(Roles.Admin) || x.Contains(Roles.Doctor))
            .WithMessage("User must be admin or doctor.");
    }
}