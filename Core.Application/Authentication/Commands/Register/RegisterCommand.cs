namespace Core.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string UserName,
    string NationalID,
    string Gender,
    string DateOfBirth,
    string BloodType,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResults>>;