namespace MedicalBlog.Application.MedicalBlog.Queries.GetPostsByTags;
using ErrorOr;
using global::MedicalBlog.Application.MedicalBlog.Common;
using MediatR;

public record GetPostsByTagQuery(
    int PageNumber,
    List<string> Tags) : IRequest<ErrorOr<List<PostResponse>>>;
