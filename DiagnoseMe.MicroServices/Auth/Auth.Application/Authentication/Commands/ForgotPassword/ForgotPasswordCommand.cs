
namespace Auth.Application.Authentication.Commands.ForgotPassword;

public record ForgotPasswordCommand(
    string Email): IRequest<ErrorOr<AuthenticationResult>>;
