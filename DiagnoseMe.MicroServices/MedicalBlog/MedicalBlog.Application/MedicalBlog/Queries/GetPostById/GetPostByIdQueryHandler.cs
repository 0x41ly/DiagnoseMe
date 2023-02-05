using ErrorOr;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;
using MediatR;
using MedicalBlog.Domain.Common.Errors;
using MapsterMapper;
namespace MedicalBlog.Application.MedicalBlog.Queries.GetPost;

public class GetPostQueryHandler : IRequestHandler<GetPostByIdQuery, ErrorOr<PostResponse>>
{
    private readonly IPostRepository _postRepository;
    private readonly IPostRatingRepository _postRatingRepository;
    private readonly IPostViewRepository _postViewRepository;
    private readonly IMapper _mapper;

    public GetPostQueryHandler(
        IPostRepository postRepository,
        ICommentAgreementRepository commentAgreementRepository,
        IPostRatingRepository postRatingRepository,
        IPostViewRepository postViewRepository,
        IMapper mapper)
    {
        _postRepository = postRepository;
        _postRatingRepository = postRatingRepository;
        _postViewRepository = postViewRepository;
        _mapper = mapper;
    }
    public async Task<ErrorOr<PostResponse>> Handle(GetPostByIdQuery query, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(query.Id);
        if (post == null)
        {
            return Errors.Post.NotFound;
        }
        var postViews = await _postViewRepository.GetByPostIdAsync(post.Id!);
        var userViewed = postViews.Any(x => x.UserId == query.UserId);
        if(!userViewed)
        {
            var postView = new PostView{
                PostId = query.Id,
                UserId = query.UserId};

            await _postViewRepository.AddAsync(postView);
            await _postViewRepository.Save();
            postViews.Add(postView);
            
        }
        var postRatings = await _postRatingRepository.GetByPostIdAsync(post.Id!);
        var comments = post.Comments.ToList();
        var viewingUsers = _mapper.Map<List<UserData>>(postViews.Select(x => x.User)); 
        var authorData = _mapper.Map<UserData>(post.Author);
        var ratingUsers = _mapper.Map<List<UserData>>(postRatings.Select(x => x.User));
        var commentsResponse = new List<CommentResponse>();
        var avgRating = postRatings.Count > 0 ? postRatings.Average(x => x.Rating) : 0;
        var reponseComments = comments
            .OrderByDescending(x => x.CreatedOn)
            .Take(20)
            .ToList();


        foreach (var comment in reponseComments)
        {
            var commentAgreementCount = comment.CommentAgreements.Count();
            var commentAgreementUsers = _mapper.Map<List<UserData>>(comment.CommentAgreements.Select(x => x.User));
            var commentAuthor = _mapper.Map<UserData>(comment.Author);
            commentsResponse.Add(new CommentResponse(
                comment.Id!,
                comment.ParentId,
                comment.Content,
                commentAuthor!,
                comment.CreatedOn.ToString(),
                comment.ModifiedOn.ToString(),
                commentAgreementCount,
                commentAgreementUsers
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

