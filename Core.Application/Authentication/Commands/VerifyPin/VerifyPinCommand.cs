
namespace Core.Application.Authentication.Commands.VerifyPin;

public record VerifyPinCommand(
    string PinCode) : IRequest<ErrorOr<string>>;