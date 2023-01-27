using ErrorOr;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;
using MediatR;
using MedicalBlog.Domain.Common.Errors;
using MapsterMapper;
namespace MedicalBlog.Application.MedicalBlog.Queries.GetPost;

public class GetPostQueryHandler : IRequestHandler<GetPostQuery, ErrorOr<PostResponse>>
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentAgreementRepository _commentAgreementRepository;
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
        _commentAgreementRepository = commentAgreementRepository;
        _postRatingRepository = postRatingRepository;
        _postViewRepository = postViewRepository;
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
        var postRatings = await _postRatingRepository.GetByPostIdAsync(post.Id!);
        var comments = post.Comments.ToList();
        var commentsId = comments.Select(x => x.Id).ToList();
        var commentsAgreements = await _commentAgreementRepository.GetCommentAgreementsByCommentsIdAsync(commentsId!);
        var viewingUsers = _mapper.Map<List<UserData>>(postViews.Select(x => x.User)); 
        var authorData = _mapper.Map<UserData>(post.Author);
        var ratingUsers = _mapper.Map<List<UserData>>(postRatings.Select(x => x.User));
        var commentsResponse = new List<CommentResponse>();
        var avgRating = postRatings.Count > 0 ? postRatings.Average(x => x.Rating) : 0;
        var reponseComments = comments
            .OrderBy(x => x.CreatedOn)
            .Take(20)
            .ToList();


        foreach (var comment in reponseComments)
        {
            var commentAgreementCount = commentsAgreements.Select(x => x.CommentId).Count();
            var commentAgreementUsers = _mapper.Map<List<UserData>>(commentsAgreements.Select(x => x.User));
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

