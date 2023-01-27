using ErrorOr;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;
using MapsterMapper;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetPostsByTags;


public class GetPostsByTagsQueryHandler : IRequestHandler<GetPostsByTagQuery, ErrorOr<List<PostResponse>>>
{
    private readonly IPostRepository _postRepository;
    private readonly IPostRatingRepository _postRatingRepository;
    private readonly IPostViewRepository _postViewRepository;
    private readonly IMapper _mapper;

    public GetPostsByTagsQueryHandler(
        IPostRepository postRepository,
        IPostRatingRepository postRatingRepository,
        IPostViewRepository postViewRepository,
        IMapper mapper)
    {
        _postRepository = postRepository;
        _postRatingRepository = postRatingRepository;
        _postViewRepository = postViewRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<PostResponse>>> Handle(GetPostsByTagQuery query, CancellationToken cancellationToken)
    {
        var posts = (await _postRepository
            .GetByTagsAsync(query.Tags))
            .OrderBy(x => x.CreatedOn)
            .Skip((query.PageNumber -1 ) * 10)
            .Take(10)
            .ToList();
        var postsId = posts.Select(x => x.Id).ToList();
        var postsViews = await _postViewRepository.GetByPostsIdAsync(postsId);
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