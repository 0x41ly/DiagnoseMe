using FluentValidation;

namespace Auth.Application.Authentication.Commands.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(command => command.UserName)
            .NotEmpty()
            .WithMessage("Username is required");
        RuleFor(command => command.OldPassword)
            .NotEmpty()
            .WithMessage("Old password is required");
        RuleFor(command => command.NewPassword)
            .NotEmpty()
            .WithMessage("New password is required");
    }
}