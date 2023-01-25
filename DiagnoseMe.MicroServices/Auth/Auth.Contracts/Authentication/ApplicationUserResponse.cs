
namespace Auth.Contracts.Authentication;


public record ApplicationUserResponse(
    string Id,
    string UserName,
    string FullName,
    string Gender,
    string DateOfBirth,
    string BloodType,
    string ProfilePic,
    string PhoneNumber,
    string NationalID,
    bool IsDoctor
);