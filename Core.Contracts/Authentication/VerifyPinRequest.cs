namespace Core.Contracts.Authentication;

public record VerifyPinRequest(
    string PinCode);