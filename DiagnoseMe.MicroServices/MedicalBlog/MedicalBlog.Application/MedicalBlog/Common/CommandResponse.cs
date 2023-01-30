namespace MedicalBlog.Application.MedicalBlog.Common;

public record CommandResponse(
    bool Success,
    string Message,
    string Path
);