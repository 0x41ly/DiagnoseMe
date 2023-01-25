using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetPost;

public record GetPostQuery(
    string Id
) : IRequest<ErrorOr<PostResponse>>;