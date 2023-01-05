
namespace Core.Application.Authentication.Commands.ResetPassword;


public record ResetPasswordCommand(
    string Id,
    string NewPassword) : IRequest<ErrorOr<AuthenticationResults>>;