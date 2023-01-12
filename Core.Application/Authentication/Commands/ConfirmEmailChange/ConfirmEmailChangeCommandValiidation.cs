using System.Text.RegularExpressions;
using Core.Domain.Common.Regexes;
using FluentValidation;

namespace Core.Application.Authentication.Commands.ConfirmEmailChange;


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
