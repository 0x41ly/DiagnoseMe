using FluentValidation;

namespace Auth.Application.Authentication.Queries.GetAllUsers;

public class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
{
    public GetAllUsersQueryValidator()
    {
        RuleFor(x => x.pageNumber).GreaterThan(0);
    }
}