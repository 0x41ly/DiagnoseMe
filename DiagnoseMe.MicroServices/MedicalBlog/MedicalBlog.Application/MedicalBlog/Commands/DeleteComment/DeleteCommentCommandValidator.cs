using FluentValidation;
using MedicalBlog.Domain.Common.Roles;

namespace MedicalBlog.Application.MedicalBlog.Commands.DeleteComment;

public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentCommandValidator()
    {
        RuleFor(x => x.CommentId)
            .NotEmpty()
            .WithMessage("CommentId is required");
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required");
        RuleFor(x => x.Roles)
            .NotEmpty()
            .WithMessage("Roles is required")
            .Must(x => x.Contains(Roles.Admin) || x.Contains(Roles.Doctor))
            .WithMessage("User must be admin or doctor");
    }
}