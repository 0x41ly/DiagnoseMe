namespace Core.Application.Authentication.Commands.AddUserToRole;

public record AddUserToRoleCommand(
    string UserName,
    string Role) : IRequest<ErrorOr<AuthenticationResults>>;