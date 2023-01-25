using FluentValidation;

namespace Auth.Application.Authentication.Commands.ConfirmEmail;

public class ConfirmEmailCommandValidation : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().
            NotNull().
            WithMessage("Id must be provided");
    }
}
