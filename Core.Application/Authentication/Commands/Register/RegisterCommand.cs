using MediatR;

namespace Core.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResults>>;