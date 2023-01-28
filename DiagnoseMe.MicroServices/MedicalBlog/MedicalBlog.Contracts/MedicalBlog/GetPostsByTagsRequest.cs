namespace MedicalBlog.Contracts.MedicalBlog;

public record GetPostsByTagsRequest(
    int PageNumber,
    List<string> Tags);