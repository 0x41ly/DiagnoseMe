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
    private readonly IMapper _mapper;
    public GetCommentsByPostIdQueryHandler(
        ICommentRepository commentRepository,
        IUserRepository userRepository, 
        IMapper mapper)
    {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<ErrorOr<List<CommentResponse>>> Handle(GetCommentsByPostIdQuery request, CancellationToken cancellationToken)
    {
        var comments = (await _commentRepository
            .GetCommentsByPostIdAsync(request.PostId))
            .Skip((request.PageNumber - 1) * 20)
            .Take(20);
        var commentAuthorsId = comments.Select(x => x.AuthorId).ToList();
        var commentAuthors = _mapper.Map<List<UserData>>((await _userRepository.GetAllAsync())
            .Where(x => commentAuthorsId.Contains(x.Id!)));
        var commentsResponse = new List<CommentResponse>();
        foreach (var comment in comments)
        {
           var commentUser = commentAuthors.Where(x => x.Id == comment.AuthorId).FirstOrDefault();
            commentsResponse.Add(new CommentResponse(
                comment.Content,
                commentUser!,
                comment.CreationDate.ToString(),
                comment.ModifiedOn.ToString()
            )); 
        }
        
        return commentsResponse;
    }
}
