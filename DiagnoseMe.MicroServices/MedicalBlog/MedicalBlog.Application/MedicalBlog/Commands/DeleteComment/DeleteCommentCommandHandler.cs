using ErrorOr;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;
using MedicalBlog.Domain.Common.Errors;
using MedicalBlog.Domain.Common.Roles;

namespace MedicalBlog.Application.MedicalBlog.Commands.DeleteComment;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, ErrorOr<CommandResponse>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPostRepository _postRepository;

    public DeleteCommentCommandHandler(
        ICommentRepository commentRepository, 
        IUserRepository userRepository,
        IPostRepository postRepository)
    {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
        _postRepository = postRepository;
    }

    public async Task<ErrorOr<CommandResponse>> Handle(DeleteCommentCommand command, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(command.CommentId);

        if (comment is null)
            return Errors.Comment.NotFound;
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user is null){
            // TODO: Check user in auth service
            return Errors.User.NotFound;
        }
        var post = await _postRepository.GetByIdAsync(comment.Post.Id);
        if (post is null)
            return Errors.Post.NotFound;
        if (comment.Author.Id != user.Id ||
            post.Author.Id != user.Id ||
            !command.Roles.Contains(Roles.Admin))
            return Errors.User.YouCanNotDoThis;

        try{
            _commentRepository.Remove(comment);
            await _commentRepository.Save();
        }
        catch (Exception)
        {
            return Errors.Comment.DeletionFailed;
        }

        return new CommandResponse(
            true,
            $"Comment with id: {comment.Id} was deleted.",
            $"/api/posts/{post.Id}"
        );
    }
}   