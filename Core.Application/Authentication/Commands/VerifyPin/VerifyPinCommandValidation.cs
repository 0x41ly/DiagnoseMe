using FluentValidation;

namespace Core.Application.Authentication.Commands.VerifyPin;


public class VerifyPinCommandValidation : AbstractValidator<VerifyPinCommand>
{
    public VerifyPinCommandValidation()
    {
        RuleFor(x => x.PinCode).NotEmpty().
            NotNull().
            WithMessage("PinCode must be provided");
    }
}
