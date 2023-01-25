using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetPostsByAuthorId;

public record GetPostsByAuthorIdQuery(
    int PageNumber,
    string AuthorId) : IRequest<ErrorOr<List<PostResponse>>>;