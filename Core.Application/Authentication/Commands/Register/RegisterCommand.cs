namespace Core.Application.Authentication.Commands.Register;

public record RegisterCommand(
    ApplicationUser User,
    string Password,
    string DateOfBirth) : IRequest<ErrorOr<AuthenticationResults>>;