namespace Core.Application.Authentication.Commands.UploadProfilePicture;

public record UploadProfilePictureCommand(
    string Base64EncodedFile,
    string UserName) : IRequest<ErrorOr<AuthenticationResults>>;