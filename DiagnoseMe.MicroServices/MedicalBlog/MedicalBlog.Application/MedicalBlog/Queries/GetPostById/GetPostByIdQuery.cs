using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetPost;

public record GetPostByIdQuery(
    string Id,
    string UserId
) : IRequest<ErrorOr<PostResponse>>;