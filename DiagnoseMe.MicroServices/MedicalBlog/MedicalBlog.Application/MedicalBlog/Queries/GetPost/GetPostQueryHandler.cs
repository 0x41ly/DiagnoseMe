using System.Collections.Generic;
using ErrorOr;
using global::MedicalBlog.Application.Common.Interfaces.Persistence;
using global::MedicalBlog.Application.MedicalBlog.Common;
using MediatR;
using global::MedicalBlog.Domain.Common.Errors;
using global::MedicalBlog.Application.MedicalBlog.Queries;
using MapsterMapper;
namespace MedicalBlog.Application.MedicalBlog.Queries.GetPost;

public class GetPostQueryHandler : IRequestHandler<GetPostQuery, ErrorOr<PostResponse>>
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRatingRepository _postRatingRepository;
    private readonly IPostViewRepository _postViewRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetPostQueryHandler(
        IPostRepository postRepository,
        ICommentRepository commentRepository,
        IPostRatingRepository postRatingRepository,
        IPostViewRepository postViewRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _postRatingRepository = postRatingRepository;
        _postViewRepository = postViewRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<ErrorOr<PostResponse>> Handle(GetPostQuery query, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(query.Id);
        if (post == null)
        {
            return Errors.Post.NotFound;
        }
        var postViews = await _postViewRepository.GetByPostIdAsync(post.Id!);
        var viewingUsersId = postViews.Select(x => x.UserId).ToList();
        var postRatings = await _postRatingRepository.GetByPostIdAsync(post.Id!);
        var ratingUsersId = postRatings.Select(x => x.UserId).ToList();
        var comments = await _commentRepository.GetCommentsByPostIdAsync(post.Id!);
        var allUsers = await _userRepository.GetAllAsync();
        var viewingUsers = _mapper.Map<List<UserData>>(allUsers.Where(x => viewingUsersId.Contains(x.Id!)));
        var authorData = _mapper.Map<UserData>(allUsers.Where(x => x.Id == post.AuthorId).FirstOrDefault()!);
        var ratingUsers = _mapper.Map<List<UserData>>(allUsers.Where(x => ratingUsersId.Contains(x.Id!)));
        var commentsResponse = new List<CommentResponse>();
        int avgRating = postRatings.Count > 0 ? (int) postRatings.Average(x => x.Rating) : 0;
        var reponseComments = comments
            .OrderBy(x => x.CreationDate)
            .Take(20)
            .ToList();
        var commentAuthorsId = reponseComments.Select(x => x.AuthorId).ToList();
        var commentAuthors = _mapper.Map<List<UserData>>(allUsers.Where(x => commentAuthorsId.Contains(x.Id!)));

        foreach (var comment in reponseComments)
        {
            var commentAuthor = commentAuthors.Where(x => x.Id == comment.AuthorId).FirstOrDefault();
            commentsResponse.Add(new CommentResponse(
                comment.Content,
                commentAuthor!,
                comment.CreationDate.ToString(),
                comment.ModifiedOn.ToString()
            ));
        }
        PostResponse postResponse = QueryHelper.MapPostResponse(
            post,
            postRatings.Count,
            comments.Count,
            authorData,
            ratingUsers,
            commentsResponse,
            postViews.Count,
            viewingUsers,
            avgRating);
        return postResponse;
    }

    
}

