using System.Text.RegularExpressions;
using Core.Domain.Common.Regexes;
using FluentValidation;

namespace Core.Application.Authentication.Commands.ResendEmailConfirmation;


public class ResendEmailConfirmationCommandValidation : AbstractValidator<ResendEmailConfirmationCommand>
{
    public ResendEmailConfirmationCommandValidation()
    {
        RuleFor(x => x.Email).NotEmpty().
            EmailAddress().
            WithMessage("The provided email is not valid");
    }
}