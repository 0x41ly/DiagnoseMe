using FluentValidation;

namespace Core.Application.Authentication.Queries.GetUsersInRole;


public class GetUsersInRoleQueryValidation : AbstractValidator<GetUsersInRoleQuery>
{
    public GetUsersInRoleQueryValidation()
    {
        RuleFor(x => x.Role)
            .NotEmpty()
            .WithMessage("Role is required");
    }
}
