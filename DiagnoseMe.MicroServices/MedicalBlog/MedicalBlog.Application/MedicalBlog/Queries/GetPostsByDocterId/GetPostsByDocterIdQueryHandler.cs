using ErrorOr;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;
using MapsterMapper;
namespace MedicalBlog.Application.MedicalBlog.Queries.GetPostsByDoctorId;

public class GetPostsByDoctorIdQueryHandler : IRequestHandler<GetPostsByDoctorIdQuery, ErrorOr<List<PostResponse>>>
{
    private readonly IPostRepository _postRepository;
    private readonly IPostRatingRepository _postRatingRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IPostViewRepository _postViewRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetPostsByDoctorIdQueryHandler(
        IPostRepository postRepository,
        IPostRatingRepository postRatingRepository,
        ICommentRepository commentRepository,
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

    public async Task<ErrorOr<List<PostResponse>>> Handle(GetPostsByDoctorIdQuery query, CancellationToken cancellationToken)
    {
        var posts = (await _postRepository
            .GetByDocterIdAsync(query.DoctorId))
            .OrderByDescending(x => x.CreatedOn)
            .Skip((query.PageNumber - 1) * 10)
            .Take(10)
            .ToList();
        var postsId = posts.Select(x => x.Id).ToList();
        var postsViews = await _postViewRepository.GetByPostsIdAsync(postsId);
        var postsIdNotViewedByUser = postsViews
            .Where(x => x.UserId != query.UserId)
            .Select(x => x.PostId)
            .ToList();
        foreach (var postId in postsIdNotViewedByUser)
        {
            var postView = await _postViewRepository.AddAsync(new PostView
            {
                PostId = postId,
                UserId = query.UserId
            });
            await _postViewRepository.AddAsync(postView);
            postsViews.Add(postView);
        }
        await _postViewRepository.Save();
        var postsRatings = await _postRatingRepository.GetByPostsIdAsync(postsId);
        var ratingUsers = _mapper.Map<List<UserData>>(postsRatings.Select(x => x.UserId).ToList());
        var postsResponse = new List<PostResponse>();
        
        foreach (var post in posts)
        {
            var comments = post.Comments;
            var postRatings = postsRatings.Where(x => x.PostId == post.Id)
                .ToList();
            var postRatingUsers = ratingUsers
                .Where(x => postRatings
                .Select(y => y.UserId)
                .Contains(x.Id))
                .ToList();
            var avgRating = postRatings.Count > 0 ? postRatings.Average(x => x.Rating) : 0;
            var postViews = postsViews
                .Where(x => x.PostId == post.Id)
                .ToList();
            var postViewingUsers = _mapper
                .Map<List<UserData>>(postViews
                .Select(x => x.UserId)
                .ToList());

            var authorData = _mapper
                .Map<UserData>(post.Author);
            postsResponse.Add(QueryHelper.MapPostResponse(
                post,
                postRatings.Count,
                comments.Count,
                authorData!,
                postRatingUsers,
                null,
                postViews.Count,
                postViewingUsers,
                avgRating));
        }
        return postsResponse;
    }
}