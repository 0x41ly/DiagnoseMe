namespace MedicalBlog.Contracts.MedicalBlog;

public record CreatePostRequest(
    string Title,
    string Content,
    List<string> Tags,
    string AuthorId);
