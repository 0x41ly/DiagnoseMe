using FluentValidation;

namespace Auth.Application.Authentication.Commands.AddUserToRole;

public class AddUserToRoleCommandValidation : AbstractValidator<AddUserToRoleCommand>
{
    public AddUserToRoleCommandValidation()
    {
        RuleFor(x => x.UserName).NotEmpty().
            NotNull().
            WithMessage("UserName must be provided");
        RuleFor(x => x.Role).NotEmpty().
            NotNull().
            WithMessage("Role must be provided");
    }
}