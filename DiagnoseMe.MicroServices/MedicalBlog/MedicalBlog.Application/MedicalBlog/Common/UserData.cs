namespace MedicalBlog.Application.MedicalBlog.Common;

public record UserData(
    string Id,
    string Name,
    string FullName,
    string ProfilePictureUrl
);