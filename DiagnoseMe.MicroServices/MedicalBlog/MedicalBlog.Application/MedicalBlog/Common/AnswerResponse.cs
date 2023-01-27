namespace MedicalBlog.Application.MedicalBlog.Common;

public record AnswerResponse(
    string Id,
    string AnswerString,
    UserData AnsweringDoctor,
    string CreatedOn,
    string? ModifiedOn,
    int AnswerAgreementsCount,
    List<UserData> AnswerAgreementUsers
);