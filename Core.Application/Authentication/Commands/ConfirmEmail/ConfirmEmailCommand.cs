namespace Core.Application.Authentication.Commands.ConfirmEmail;

public record ConfirmEmailCommand(
    string Id) : IRequest<ErrorOr<AuthenticationResults>>;

