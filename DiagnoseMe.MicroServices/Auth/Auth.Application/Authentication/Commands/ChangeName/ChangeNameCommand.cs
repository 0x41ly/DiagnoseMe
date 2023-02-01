namespace Auth.Application.Authentication.Commands.ChangeName;

public record ChangeNameCommand(
    string UserName,
    string NewUserName
): IRequest<ErrorOr<AuthenticationResult>>;