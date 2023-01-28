using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetQuestion;

public record GetQuestionQuery(
    string QuestionId) : IRequest<ErrorOr<QuestionResponse>>;