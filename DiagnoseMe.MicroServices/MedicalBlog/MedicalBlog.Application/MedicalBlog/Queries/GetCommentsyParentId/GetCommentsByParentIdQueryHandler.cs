using ErrorOr;
using MapsterMapper;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetCommentsyParentId;

public class GetCommentsByPostIdQueryHandler : IRequestHandler<GetCommentsByParentIdQuery, ErrorOr<List<CommentResponse>>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;
    public GetCommentsByPostIdQueryHandler(
        ICommentRepository commentRepository,
        IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }
    public async Task<ErrorOr<List<CommentResponse>>> Handle(GetCommentsByParentIdQuery query, CancellationToken cancellationToken)
    {
        var comments = (await _commentRepository
            .GetByParentIdAsync(query.ParentId))
            .OrderByDescending(x => x.CreatedOn)
            .Skip((query.PageNumber - 1) * 20)
            .Take(20);
        var commentsResponse = new List<CommentResponse>();
        foreach (var comment in comments)
        {
            var commentAgreementCount = comment.CommentAgreements.Count();
            var commentAuthor = _mapper.Map<UserData>(comment.Author);
            var commentAgreementUsers = _mapper.Map<List<UserData>>(comment.CommentAgreements.Select(x => x.User));
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