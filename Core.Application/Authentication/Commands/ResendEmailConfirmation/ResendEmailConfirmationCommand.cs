
namespace Core.Application.Authentication.Commands.ResendEmailConfirmation;

public record ResendEmailConfirmationCommand(
    string Email
) : IRequest<ErrorOr<AuthenticationResults>>;