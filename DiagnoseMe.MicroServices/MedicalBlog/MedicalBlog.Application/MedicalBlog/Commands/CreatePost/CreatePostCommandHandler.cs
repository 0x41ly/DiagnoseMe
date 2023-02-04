using ErrorOr;
using MapsterMapper;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;
using MedicalBlog.Domain.Common.Errors;

namespace MedicalBlog.Application.MedicalBlog.Commands.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ErrorOr<CommandResponse>>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public CreatePostCommandHandler(IPostRepository postRepository,
        IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<CommandResponse>> Handle(CreatePostCommand command, CancellationToken cancellationToken)
    {
        var author = await _userRepository.GetByIdAsync(command.AuthorId);
        if (author == null)
        {
            // TODO: get the user from the auth service and add it to the db
            return Errors.User.SomethingWentWrong;
        }

        Post post = new Post{
            Id = Guid.NewGuid().ToString(),
            Title = command.Title,
            Content = command.Content,
            Tags = string.Join(',',command.Tags),
            AuthorId = command.AuthorId,
            CreatedOn = DateTime.UtcNow
        };

        try
        {
            await _postRepository.AddAsync(post);
            await _postRepository.Save();
        }
        catch
        {
            return Errors.Post.CreationFailed;
        }

        return new CommandResponse(
            true,
            "Post created successfully",
            $"/api/posts/{post.Id}");
    }
}