namespace Core.Application.Authentication.Commands.AddUserToRole;

public class AddUserToRoleCommandHandler :
    BaseHandler,
    IRequestHandler<AddUserToRoleCommand, ErrorOr<AuthenticationResults>>
{
    public AddUserToRoleCommandHandler(
        UserManager<ApplicationUser> userManager
    ): base(userManager){}

    public async Task<ErrorOr<AuthenticationResults>> Handle(AddUserToRoleCommand command, CancellationToken cancellationToken)
    {
        var results = new AuthenticationResults();
        if (!Roles.AvailableRoles.Contains(command.Role))
            return Errors.Role.RoleNotExist;

        var user = await _userManager.FindByNameAsync(command.UserName);
        if (user == null)
            return Errors.User.Name.NotExist;

        var result = await _userManager.AddToRoleAsync(user, command.Role);
        if (!result.Succeeded)
            return Errors.Role.FialToAdd;

        results.Message = "User added to role successfully";
        return results;
    }
}