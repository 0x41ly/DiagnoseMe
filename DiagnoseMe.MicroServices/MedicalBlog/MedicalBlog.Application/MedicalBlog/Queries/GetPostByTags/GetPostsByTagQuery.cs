namespace MedicalBlog.Application.MedicalBlog.Queries.GetPostByTags;
using ErrorOr;
using global::MedicalBlog.Application.MedicalBlog.Common;
using MediatR;

public record GetPostsByTagQuery(
    int PageNumber,
    List<string> Tags) : IRequest<ErrorOr<List<PostResponse>>>;
