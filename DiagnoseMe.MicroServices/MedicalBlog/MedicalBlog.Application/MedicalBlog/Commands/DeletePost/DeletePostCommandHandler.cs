using ErrorOr;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;
using MedicalBlog.Domain.Common.Errors;

namespace MedicalBlog.Application.MedicalBlog.Commands.DeletePost;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, ErrorOr<CommandResponse>>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public DeletePostCommandHandler(
        IPostRepository postRepository, 
        IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<CommandResponse>> Handle(DeletePostCommand command, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(command.PostId);

        if (post is null)
            return Errors.Post.NotFound;
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user is null)
            return Errors.User.NotFound;
        if (post.Author.Id != user.Id)
            return Errors.Post.YouCanNotDoThis;

        try{
            _postRepository.Remove(post);
            await _postRepository.Save();
        }
        catch (Exception)
        {
            return Errors.Post.DeletionFailed;
        }

        return new CommandResponse(
            true,
            $"Post with id: {post.Id} was deleted.",
            "/medical-blog/posts"
        );
    }
}