namespace MedicalBlog.Application.MedicalBlog.Queries.GetPost;
using ErrorOr;
using global::MedicalBlog.Application.Common.Interfaces.Persistence;
using global::MedicalBlog.Application.MedicalBlog.Common;
using MediatR;
using global::MedicalBlog.Domain.Common.Errors;
using global::MedicalBlog.Application.MedicalBlog.Queries;

public class GetPostQueryHandler : IRequestHandler<GetPostQuery, ErrorOr<PostResponse>>
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IPostSuggestionRepository _postSuggestionRepository;
    public GetPostQueryHandler(
        IPostRepository postRepository,
        ICommentRepository commentRepository,
        IPostSuggestionRepository postSuggestionRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _postSuggestionRepository = postSuggestionRepository;
    }
    public async Task<ErrorOr<PostResponse>> Handle(GetPostQuery query, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(query.Id);
        if (post == null)
        {
            return Errors.Post.NotFound;
        }
        var postSuggestion = await _postSuggestionRepository.GetByPostIdAsync(post.Id!);
        var suggestingUsersId = postSuggestion.Select(x => x.UserId).ToList();
        var comments = await _commentRepository.GetCommentsByPostIdAsync(post.Id!);
        var authorData = new UserData("", "", ""); // TODO: Feach user data from Auth service
        var suggestingUsers = new List<UserData>(); // TODO: Feach users data from Auth service
        var commentsResponse = new List<CommentResponse>();
        var commentUsersId = comments.Select(x => x.AuthorId).ToList();
        var commentUsers = new List<UserData>(); // TODO: Feach users data from Auth service
        foreach (var comment in comments)
        {
            var commentUser = commentUsers.Where(x => x.Id == comment.AuthorId).FirstOrDefault();
            commentsResponse.Add(new CommentResponse(
                comment.Content,
                commentUser!,
                comment.CreationDate.ToString(),
                comment.ModifiedOn.ToString()
            ));
        }
        PostResponse postResponse = QueryHelper.MapPostResponse(
            post,
            postSuggestion,
            comments,
            authorData,
            suggestingUsers,
            commentsResponse);
        return postResponse;
    }

    
}

