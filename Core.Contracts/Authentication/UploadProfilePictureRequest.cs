namespace Core.Contracts.Authentication;

public record UploadProfilePictureRequest(
    string Base64EncodedFile
);