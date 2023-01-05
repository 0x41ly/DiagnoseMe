using MediatR;
namespace Core.Application.Authentication.Commands.ConfirmEmailChange;

public record ConfirmEmailChangeCommand(
    string NewEmail,
    string Id) : IRequest<ErrorOr<AuthenticationResults>>;

