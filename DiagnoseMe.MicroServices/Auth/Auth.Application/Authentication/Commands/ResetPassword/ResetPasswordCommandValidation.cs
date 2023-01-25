using FluentValidation;

namespace Auth.Application.Authentication.Commands.ResetPassword;


public class ResetPasswordCommandValidation : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().
            NotNull().
            WithMessage("Id must be provided");
        RuleFor(x => x.NewPassword).NotEmpty().
            NotNull().
            WithMessage("New Password must be provided");
    }
}
