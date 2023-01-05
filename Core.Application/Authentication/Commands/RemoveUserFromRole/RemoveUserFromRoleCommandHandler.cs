
namespace Core.Application.Authentication.Commands.RemoveUserFromRole;

public class RemoveUserFromRoleCommandHandler :
    BaseHandler,
    IRequestHandler<RemoveUserFromRoleCommand, ErrorOr<AuthenticationResults>>
{
    public RemoveUserFromRoleCommandHandler(
        UserManager<ApplicationUser> userManager
    ): base(userManager){}

    public async Task<ErrorOr<AuthenticationResults>> Handle(RemoveUserFromRoleCommand command, CancellationToken cancellationToken)
    {
        var results = new AuthenticationResults();
        if (!Roles.AvailableRoles.Contains(command.Role))
            return Errors.Role.RoleNotExist;

        var user = await _userManager.FindByNameAsync(command.UserName);
        if (user == null)
            return Errors.User.Name.NotExist;
        

        var result = await _userManager.RemoveFromRoleAsync(user, command.Role);
        if (!result.Succeeded)
            return Errors.Role.FialToRemove;
        
        results.Message = "User removed from role successfully";
        return results;
    }
}
