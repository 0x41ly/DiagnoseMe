using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetQuestions;

public record GetQuestionsQuery(
    int PageNumber) : IRequest<ErrorOr<List<QuestionResponse>>>;
