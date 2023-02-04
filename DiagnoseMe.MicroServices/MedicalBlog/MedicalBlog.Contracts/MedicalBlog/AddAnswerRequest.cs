namespace MedicalBlog.Contracts.MedicalBlog;

public record AddAnswerRequest(
    string PostId,
    string CommentId,
    string AnswerString);