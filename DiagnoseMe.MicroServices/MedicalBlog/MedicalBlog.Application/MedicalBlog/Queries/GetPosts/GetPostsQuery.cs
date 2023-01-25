using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetPosts;

public record GetPostsQuery(
    int PageNumber
) : IRequest<List<PostResponse>>;