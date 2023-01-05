namespace Core.Application.Authentication.Queries.GetUsersInRole;


public record GetUsersInRoleQuery(
    string Role) : IRequest<ErrorOr<List<ApplicationUser>>>;