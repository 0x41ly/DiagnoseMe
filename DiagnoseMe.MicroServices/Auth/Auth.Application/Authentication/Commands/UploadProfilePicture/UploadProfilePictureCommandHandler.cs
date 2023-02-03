using Auth.Application.Common.Interfaces.Services;
using FileTypeChecker.Extensions;

namespace Auth.Application.Authentication.Commands.UploadProfilePicture;

public class ResetPasswordCommandHandle :
    BaseAuthenticationHandler,
    IRequestHandler<UploadProfilePictureCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IFileHandler _fileHandler;
    public ResetPasswordCommandHandle(
        UserManager<ApplicationUser> userManager,
        IFileHandler fileHandler): base(userManager){
            _fileHandler = fileHandler;
        }
    public async Task<ErrorOr<AuthenticationResult>> Handle(UploadProfilePictureCommand command, CancellationToken cancellationToken)
    {


            var file = Convert.FromBase64String(command.Base64EncodedFile);
            Stream fileStream = new MemoryStream(file);
            if (fileStream.IsImage())
                return (Errors.User.File.NotImage);

            var result = _fileHandler.SaveFile(file);
            if (result.IsError)
                return (Errors.UnExpected);
            var user = await _userManager.FindByNameAsync(command.UserName);
            user!.ProfilePictureUrl = result.Value;
            var updateResult = await _userManager.UpdateAsync(user);
            
        return (
            new AuthenticationResult{
                Message = "Profile picture have been successfully changed"});
   }
}