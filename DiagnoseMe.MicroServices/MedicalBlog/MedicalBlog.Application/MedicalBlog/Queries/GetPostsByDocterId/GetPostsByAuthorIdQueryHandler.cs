using ErrorOr;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetPostsByAuthorId;

public class GetPostsByAuthorIdQueryHandler : IRequestHandler<GetPostsByAuthorIdQuery, ErrorOr<List<PostResponse>>>
{
    private readonly IPostRepository _postRepository;
    private readonly IPostSuggestionRepository _postSuggestionRepository;
    private readonly ICommentRepository _commentRepository;

    public GetPostsByAuthorIdQueryHandler(
        IPostRepository postRepository,
        IPostSuggestionRepository postSuggestionRepository,
        ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _postSuggestionRepository = postSuggestionRepository;
    }

    public async Task<ErrorOr<List<PostResponse>>> Handle(GetPostsByAuthorIdQuery request, CancellationToken cancellationToken)
    {
        var posts = (await _postRepository
            .GetByDocterIdAsync(request.AuthorId))
            .OrderBy(x => x.CreationDate)
            .Skip((request.PageNumber - 1) * 10)
            .Take(10)
            .ToList();
        var postsId = posts.Select(x => x.Id).ToList();
        var postsSuggestions = await _postSuggestionRepository.GetByPostsIdAsync(postsId);
        var suggestingUsersId = postsSuggestions.Select(x => x.UserId).ToList();
        var authorsData = new List<UserData>(); // TODO: Feach users data from Auth service
        var suggestingUsers = new List<UserData>(); // TODO: Feach users data from Auth service
        var postsResponse = new List<PostResponse>();
        foreach (var post in posts)
        {
            var comments = await _commentRepository.GetCommentsByPostIdAsync(post.Id!);
            var postSuggestions = postsSuggestions.Where(x => x.PostId == post.Id)
                .ToList();
            var postSuggestingUsers = suggestingUsers
                .Where(x => postSuggestions.Select(y => y.UserId).Contains(x.Id))
                .ToList();
            var authorData = authorsData.Where(x => x.Id == post.AuthorId).FirstOrDefault();
            postsResponse.Add(QueryHelper.MapPostResponse(
                post,
                postSuggestions,
                comments,
                authorData!,
                postSuggestingUsers,
                null));
        }
        return postsResponse;
        
    }
}