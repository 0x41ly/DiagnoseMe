namespace Core.Application.Authentication.Commands.ChangeEmail;

public record ChangeEmailCommand(
    string UserName,
    string NewEmail
): IRequest<ErrorOr<AuthenticationResults>>;