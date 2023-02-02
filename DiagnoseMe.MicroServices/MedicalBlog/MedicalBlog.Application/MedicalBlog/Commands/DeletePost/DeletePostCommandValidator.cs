using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Commands.DeletePost;

public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
{
    public DeletePostCommandValidator()
    {
        RuleFor(command => command.PostId)
            .NotEmpty()
            .WithMessage("PostId is required");
        RuleFor(command => command.UserId)
            .NotEmpty()
            .WithMessage("UserId is required")
            .NotNull()
            .WithMessage("UserId is required");
        RuleFor(command => command.Roles)
            .NotEmpty()
            .WithMessage("Roles is required")
            .NotNull()
            .WithMessage("Roles is required");
    }
}