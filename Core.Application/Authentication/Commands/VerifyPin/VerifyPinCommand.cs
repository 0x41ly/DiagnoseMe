
namespace Core.Application.Authentication.Commands.VerifyPin;

public record VerifyPinCommand(
    string Pincode
) : IRequest<ErrorOr<string>>;