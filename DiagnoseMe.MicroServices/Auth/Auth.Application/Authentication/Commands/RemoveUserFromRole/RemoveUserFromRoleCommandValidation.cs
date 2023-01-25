using FluentValidation;

namespace Auth.Application.Authentication.Commands.RemoveUserFromRole;


public class RemoveUserFromRoleCommandValidation : AbstractValidator<RemoveUserFromRoleCommand>
{
    public RemoveUserFromRoleCommandValidation()
    {
        RuleFor(x => x.UserName).NotEmpty().
            NotNull().
            WithMessage("UserName must be provided");
        RuleFor(x => x.Role).NotEmpty().
            NotNull().
            WithMessage("Role must be provided");
    }
}