using System.Collections.Generic;
using ErrorOr;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;
using MapsterMapper;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetPostByTags;


public class GetPostsByTagsQueryHandler : IRequestHandler<GetPostsByTagQuery, ErrorOr<List<PostResponse>>>
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRatingRepository _postRatingRepository;
    private readonly IPostViewRepository _postViewRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetPostsByTagsQueryHandler(
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
        var ViewingUsersId = postsViews.Select(x => x.UserId).ToList();
        var allUsers = _mapper.Map<List<UserData>>(await _userRepository.GetAllAsync());
        var viewingUsers = allUsers.Where(x => ViewingUsersId.Contains(x.Id!));
        var postsRatings = await _postRatingRepository.GetByPostsIdAsync(postsId);
        var ratingUsersId = postsRatings.Select(x => x.UserId).ToList();
        var authorsData = allUsers.Where(x => posts.Select(y => y.AuthorId).Contains(x.Id!));
        var ratingUsers = allUsers.Where(x => ratingUsersId.Contains(x.Id!));
        var postsResponse = new List<PostResponse>();
        foreach (var post in posts)
        {
            var comments = await _commentRepository.GetCommentsByPostIdAsync(post.Id!);
            var postRatings = postsRatings.Where(x => x.PostId == post.Id)
                .ToList();
            var postRatingUsers = ratingUsers
                .Where(x => postRatings.Select(y => y.UserId).Contains(x.Id))
                .ToList();
            var avgRating = postRatings.Count > 0 ? postRatings.Average(x => x.Rating) : 0;
            var postViews = postsViews.Where(x => x.PostId == post.Id)
                .ToList();
            var postViewingUsers = viewingUsers
                .Where(x => postViews.Select(y => y.UserId).Contains(x.Id))
                .ToList();

            var authorData = authorsData.Where(x => x.Id == post.AuthorId).FirstOrDefault();
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