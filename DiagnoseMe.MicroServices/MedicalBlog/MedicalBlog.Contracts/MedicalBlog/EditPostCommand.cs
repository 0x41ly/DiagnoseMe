namespace MedicalBlog.Contracts.MedicalBlog;

public record EditPostRequest(
    string Title,
    string Content,
    List<string> Tags);