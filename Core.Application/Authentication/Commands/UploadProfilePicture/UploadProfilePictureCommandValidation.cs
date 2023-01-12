using System.Text.RegularExpressions;
using Core.Domain.Common.Regexes;
using FluentValidation;

namespace Core.Application.Authentication.Commands.UploadProfilePicture;

public class UploadProfilePictureCommandValidation : AbstractValidator<UploadProfilePictureCommand>
{
    public UploadProfilePictureCommandValidation()
    {
        RuleFor(x => x.Base64EncodedFile).NotEmpty().
            Must(x =>Regex.IsMatch(x, Regexes.Base64Regex)).
            WithMessage("The provided file is not valid base64");
        RuleFor(x => x.UserName).NotEmpty().
            NotNull().
            WithMessage("UserName must be provided");
    }
}