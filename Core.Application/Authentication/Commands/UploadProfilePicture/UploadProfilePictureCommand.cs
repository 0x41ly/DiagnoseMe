namespace Core.Application.Authentication.Commands.UploadProfilePicture;

public record UploadProfilePictureCommand(
    string Base64EncodedFile) : IRequest<ErrorOr<AuthenticationResults>>;