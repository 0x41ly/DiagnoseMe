namespace Auth.Contracts.Authentication;

public record VerifyPinRequest(
    string PinCode);