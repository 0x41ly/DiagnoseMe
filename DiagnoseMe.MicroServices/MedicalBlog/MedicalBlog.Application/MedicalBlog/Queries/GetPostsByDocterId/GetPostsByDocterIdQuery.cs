using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetPostsByDoctorId;

public record GetPostsByDoctorIdQuery(
    string DoctorId,
    int PageNumber,
    string UserId) : IRequest<ErrorOr<List<PostResponse>>>;