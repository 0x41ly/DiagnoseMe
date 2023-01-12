using System.Text.RegularExpressions;
using Core.Domain.Common.Regexes;
using FluentValidation;

namespace Core.Application.Authentication.Commands.ForgotPassword;


public class ForgotPasswordCommandValidation : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidation()
    {
        RuleFor(x => x.Email).NotEmpty().
            EmailAddress().
            WithMessage("The provided email is not valid");
    }
}
