namespace MedicalBlog.Application.MedicalBlog.Queries.GetPostsByTags;
using ErrorOr;
using global::MedicalBlog.Application.MedicalBlog.Common;
using MediatR;

public record GetPostsByTagsQuery(
    int PageNumber,
    List<string> Tags) : IRequest<ErrorOr<List<PostResponse>>>;
