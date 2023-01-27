namespace MedicalBlog.Application.MedicalBlog.Common;

public record QuestionResponse(
    string Id,
    string QuestionString,
    UserData AskingUser,
    string CreatedOn,
    string? ModifiedOn,
    List<AnswerResponse>? Answers,
    int AnswersCount
);