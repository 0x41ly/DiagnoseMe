namespace Auth.Application.Authentication.Commands.ChangePassword;

public record ChangePasswordCommand(
    string UserName,
    string OldPassword,
    string NewPassword
) : IRequest<ErrorOr<AuthenticationResult>>;