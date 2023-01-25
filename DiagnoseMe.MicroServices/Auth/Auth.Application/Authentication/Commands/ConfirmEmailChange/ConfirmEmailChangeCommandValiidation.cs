using System.Text.RegularExpressions;
using Auth.Domain.Common.Regexes;
using FluentValidation;

namespace Auth.Application.Authentication.Commands.ConfirmEmailChange;


public class ConfirmEmailChangeCommandValiidation : AbstractValidator<ConfirmEmailChangeCommand>
{
    public ConfirmEmailChangeCommandValiidation()
    {
        RuleFor(x => x.NewEmail).NotEmpty().
            EmailAddress().
            WithMessage("The provided email is not valid");
        RuleFor(x => x.Id).NotEmpty().
            NotNull().
            WithMessage("Id must be provided");
    }
}
