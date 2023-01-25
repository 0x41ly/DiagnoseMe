using FluentValidation;

namespace Auth.Application.Authentication.Commands.ChangeName;

public class ChangeNameCommandValidation : AbstractValidator<ChangeNameCommand>
{
    public ChangeNameCommandValidation()
    {
        RuleFor(x => x.UserName).NotEmpty().
            NotNull().
            WithMessage("UserName must be provided");
        RuleFor(x => x.NewUserName).NotEmpty().
            NotNull().
            WithMessage("New UserName must be provided");
    }
}