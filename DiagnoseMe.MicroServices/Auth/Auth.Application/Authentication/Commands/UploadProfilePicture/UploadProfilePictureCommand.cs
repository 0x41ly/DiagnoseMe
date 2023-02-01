namespace Auth.Application.Authentication.Commands.UploadProfilePicture;

public record UploadProfilePictureCommand(
    string Base64EncodedFile,
    string UserName) : IRequest<ErrorOr<AuthenticationResult>>;