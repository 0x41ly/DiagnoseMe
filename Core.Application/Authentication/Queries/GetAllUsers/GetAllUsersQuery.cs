namespace Core.Application.Authentication.Queries.GetAllUsers;

public record GetAllUsersQuery() : IRequest<List<ApplicationUser>>;