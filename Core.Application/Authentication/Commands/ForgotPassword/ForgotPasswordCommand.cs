
namespace Core.Application.Authentication.Commands.ForgotPassword;

public record ForgotPasswordCommand(
    string Email): IRequest<ErrorOr<AuthenticationResults>>;
