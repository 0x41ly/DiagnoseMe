using MediatR;
namespace Auth.Application.Authentication.Commands.ConfirmEmailChange;

public record ConfirmEmailChangeCommand(
    string NewEmail,
    string Id) : IRequest<ErrorOr<AuthenticationResult>>;

