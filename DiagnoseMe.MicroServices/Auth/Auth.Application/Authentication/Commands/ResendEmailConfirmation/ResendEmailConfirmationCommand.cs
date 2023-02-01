
namespace Auth.Application.Authentication.Commands.ResendEmailConfirmation;

public record ResendEmailConfirmationCommand(
    string Email
) : IRequest<ErrorOr<AuthenticationResult>>;