using System;


namespace Auth.Contracts.Authentication;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string UserName,
    string NationalID,
    string Gender,
    string DateOfbirth,
    string BloodType,
    string Email,
    string Password);

