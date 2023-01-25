using FluentValidation;

namespace Auth.Application.Authentication.Queries.GetToken;


public class GetTokenQueryValidation : AbstractValidator<GetTokenQuery>
{
    public GetTokenQueryValidation()
    {
        RuleFor(x => x.Email).
            NotEmpty().
            EmailAddress().
            WithMessage("The provided email is not valid");

        RuleFor(x => x.Password).
            NotEmpty().
            WithMessage("Password must be provided");
    }
}