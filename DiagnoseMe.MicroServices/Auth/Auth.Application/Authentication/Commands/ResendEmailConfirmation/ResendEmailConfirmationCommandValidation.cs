using System.Text.RegularExpressions;
using Auth.Domain.Common.Regexes;
using FluentValidation;

namespace Auth.Application.Authentication.Commands.ResendEmailConfirmation;


public class ResendEmailConfirmationCommandValidation : AbstractValidator<ResendEmailConfirmationCommand>
{
    public ResendEmailConfirmationCommandValidation()
    {
        RuleFor(x => x.Email).NotEmpty().
            EmailAddress().
            WithMessage("The provided email is not valid");
    }
}