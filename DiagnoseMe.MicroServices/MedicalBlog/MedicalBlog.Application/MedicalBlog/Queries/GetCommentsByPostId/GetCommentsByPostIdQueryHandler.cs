using ErrorOr;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;
using MediatR;
using MapsterMapper;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetCommentsByPostId;

public class GetCommentsByPostIdQueryHandler : IRequestHandler<GetCommentsByPostIdQuery, ErrorOr<List<CommentResponse>>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ICommentAgreementRepository _commentAgreementRepository;
    private readonly IMapper _mapper;
    public GetCommentsByPostIdQueryHandler(
        ICommentRepository commentRepository,
        ICommentAgreementRepository commentAgreementRepository,
        IMapper mapper)
    {
        _commentRepository = commentRepository;
        _commentAgreementRepository = commentAgreementRepository;
        _mapper = mapper;
    }
    public async Task<ErrorOr<List<CommentResponse>>> Handle(GetCommentsByPostIdQuery query, CancellationToken cancellationToken)
    {
        var comments = (await _commentRepository
            .GetCommentsByPostIdAsync(query.PostId))
            .Skip((query.PageNumber - 1) * 20)
            .Take(20);
        var commentsId = comments.Select(x => x.Id).ToList();
        var commentsAuthors = _mapper.Map<List<UserData>>(comments.Select(x => x.Author).ToList());
        var commentsAgreements = await _commentAgreementRepository.GetCommentAgreementsByCommentsIdAsync(commentsId!);
        var commentsResponse = new List<CommentResponse>();
        foreach (var comment in comments)
        {
            var commentAgreementCount = commentsAgreements.Select(x => x.CommentId).Count();
            var commentAuthor = commentsAuthors.Where(x => x.Id == comment.AuthorId).FirstOrDefault();
            var commentAgreementUsers = _mapper.Map<List<UserData>>(commentsAgreements.Select(x => x.User));
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
        
        return commentsResponse;
    }
}
