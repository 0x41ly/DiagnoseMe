using System.Text.RegularExpressions;
using Auth.Domain.Common.BloodTypes;
using Auth.Domain.Common.Regexes;
using FluentValidation;

namespace Auth.Application.Authentication.Commands.Register;
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.User.FirstName).NotEmpty().
            NotNull().
            WithMessage("FirstName must be provided");
        
        RuleFor(x => x.User.LastName).NotEmpty().
            NotNull().
            WithMessage("LastName must be provided");
            
        RuleFor(x => x.User.UserName).NotEmpty().
            NotNull().
            WithMessage("UserName must be provided");

        RuleFor(x => x.User.Gender).NotEmpty().
            NotNull().
            WithMessage("Gender must be provided");
        
        RuleFor(x => x.User.NationalID).NotEmpty().
            Must(x => Regex.IsMatch(x, Regexes.NationalIDRegex)).
            WithMessage("NationalID must be 14 digits");

        RuleFor(x => x.User.Email).NotEmpty().
            EmailAddress().
            WithMessage("The provided email is not valid");
        
        RuleFor(x => x.DateOfBirth).NotEmpty().
            Must(x => Regex.IsMatch(x, Regexes.DateRegex)).
            WithMessage("Date must be provided in 'yyyy-MM-dd'format");

        RuleFor(x => x.User.BloodType).NotEmpty().
            Must(x => BloodTypes.bloodTypeList.Contains(x)).
            WithMessage($"BloodType must be one of the following {'{'+String.Join(",",BloodTypes.bloodTypeList)+'}'}");
    }

}