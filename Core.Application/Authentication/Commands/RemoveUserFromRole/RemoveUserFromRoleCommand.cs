
namespace Core.Application.Authentication.Commands.RemoveUserFromRole;

public record RemoveUserFromRoleCommand(
    string UserName,
    string Role) : IRequest<ErrorOr<AuthenticationResults>>;
