using System.Text.RegularExpressions;
using Auth.Domain.Common.Regexes;
using FluentValidation;

namespace Auth.Application.Authentication.Commands.ChangeEmail;

public class ChangeEmailCommandValidation : AbstractValidator<ChangeEmailCommand>
{
    public ChangeEmailCommandValidation()
    {
        RuleFor(x => x.UserName).NotEmpty().
            NotNull().
            WithMessage("UserName must be provided");
        RuleFor(x => x.NewEmail).NotEmpty().
            EmailAddress().
            WithMessage("The provided email is not valid");
    }
    
}