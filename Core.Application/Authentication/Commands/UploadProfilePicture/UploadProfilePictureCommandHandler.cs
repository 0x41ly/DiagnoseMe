using Core.Application.Common.Interfaces.Services;
using FileTypeChecker.Extensions;

namespace Core.Application.Authentication.Commands.UploadProfilePicture;

public class ResetPasswordCommandHandle :
    BaseAuthenticationHandler,
    IRequestHandler<UploadProfilePictureCommand, ErrorOr<AuthenticationResults>>
{
    private readonly IFileHandler _fileHandler;
    public ResetPasswordCommandHandle(
        UserManager<ApplicationUser> userManager,
        IFileHandler fileHandler): base(userManager){
            _fileHandler = fileHandler;
        }
    public Task<ErrorOr<AuthenticationResults>> Handle(UploadProfilePictureCommand command, CancellationToken cancellationToken)
    {

        try{
            var file = Convert.FromBase64String(command.Base64EncodedFile);
            Stream fileStream = new MemoryStream(file);
            if (fileStream.IsImage())
                return Task.FromResult<ErrorOr<AuthenticationResults>>(Errors.User.File.NotImage);

            var result = _fileHandler.SaveFile(file);
            if (result.IsError)
                return Task.FromResult<ErrorOr<AuthenticationResults>>(Errors.UnExpected);
                
        }
        catch{
            return Task.FromResult<ErrorOr<AuthenticationResults>>(Errors.User.File.NotB64);
        }
        return Task.FromResult<ErrorOr<AuthenticationResults>>(
            new AuthenticationResults{
                Message = "Profile picture have been successfully changed"
        });
   }
}