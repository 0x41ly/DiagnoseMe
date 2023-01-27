using System.Collections.Generic;
using ErrorOr;
using global::MedicalBlog.Application.Common.Interfaces.Persistence;
using global::MedicalBlog.Application.MedicalBlog.Common;
using MediatR;
using global::MedicalBlog.Domain.Common.Errors;
using global::MedicalBlog.Application.MedicalBlog.Queries;
using MapsterMapper;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetCommentsByPostId;

public class GetCommentsByPostIdQueryHandler : IRequestHandler<GetCommentsByPostIdQuery, ErrorOr<List<CommentResponse>>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICommentAgreementRepository _commentAgreementRepository;
    private readonly IMapper _mapper;
    public GetCommentsByPostIdQueryHandler(
        ICommentRepository commentRepository,
        ICommentAgreementRepository commentAgreementRepository,
        IUserRepository userRepository, 
        IMapper mapper)
    {
        _commentRepository = commentRepository;
        _commentAgreementRepository = commentAgreementRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<ErrorOr<List<CommentResponse>>> Handle(GetCommentsByPostIdQuery query, CancellationToken cancellationToken)
    {
        var comments = (await _commentRepository
            .GetCommentsByPostIdAsync(query.PostId))
            .Skip((query.PageNumber - 1) * 20)
            .Take(20);
        var commentsId = comments.Select(x => x.Id).ToList();
        var commentAuthorsId = comments.Select(x => x.AuthorId).ToList();
        var allUsers = _mapper.Map<List<UserData>>(await _userRepository.GetAllAsync());
        var commentsAuthors = allUsers
            .Where(x => commentAuthorsId.Contains(x.Id!));
        var commentsAgreements = await _commentAgreementRepository.GetCommentAgreementsByCommentsIdAsync(commentsId!);
        var commentsResponse = new List<CommentResponse>();
        foreach (var comment in comments)
        {
            var commentAgreementCount = commentsAgreements.Select(x => x.CommentId).Count();
            var commentAuthor = commentsAuthors.Where(x => x.Id == comment.AuthorId).FirstOrDefault();
            var commentAgreementUsers = allUsers
                .Where(x => commentsAgreements.Select(y => y.UserId).Contains(x.Id!))
                .ToList();
            commentsResponse.Add(new CommentResponse(
                comment.Id!,
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
