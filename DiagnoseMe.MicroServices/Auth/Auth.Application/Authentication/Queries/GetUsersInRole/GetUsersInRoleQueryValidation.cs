using FluentValidation;

namespace Auth.Application.Authentication.Queries.GetUsersInRole;


public class GetUsersInRoleQueryValidation : AbstractValidator<GetUsersInRoleQuery>
{
    public GetUsersInRoleQueryValidation()
    {
        RuleFor(x => x.Role)
            .NotEmpty()
            .WithMessage("Role is required");
        RuleFor(x => x.pageNumber).GreaterThanOrEqualTo(0);
    }
}
