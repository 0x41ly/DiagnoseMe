namespace Core.Application.Authentication.Queries.GetAllUsers;

public class GetAllUsersQueryHandler :
    BaseHandler,
    IRequestHandler<GetAllUsersQuery, List<ApplicationUser>>
{
    public GetAllUsersQueryHandler(
        UserManager<ApplicationUser> userManager
    ): base(userManager){}
    public Task<List<ApplicationUser>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        return Task.FromResult<List<ApplicationUser>>(_userManager.Users.AsParallel().ToList());
    }
    
}