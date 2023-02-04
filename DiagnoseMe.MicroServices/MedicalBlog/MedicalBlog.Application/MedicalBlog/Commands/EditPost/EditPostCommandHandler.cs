using ErrorOr;
using MapsterMapper;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;
using MedicalBlog.Domain.Common.Errors;

namespace MedicalBlog.Application.MedicalBlog.Commands.EditPost;

public class EditPostCommandHandler : IRequestHandler<EditPostCommand, ErrorOr<CommandResponse>>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public EditPostCommandHandler(IPostRepository postRepository,
        IMapper mapper,
        IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<CommandResponse>> Handle(EditPostCommand command, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(command.PostId);
        if (post == null)
        {
            return Errors.Post.NotFound;
        }

        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user == null)
        {
            return Errors.User.NotFound;
        }

        if (post.AuthorId != user.Id)
        {
            return Errors.User.YouCanNotDoThis;
        }

        post.Title = command.Title;
        post.Content = command.Content;
        post.Tags = string.Join(',', command.Tags);
        post.ModifiedOn = DateTime.UtcNow;

        try
        {
            await _postRepository.Edit(post);
            await _postRepository.Save();
        }
        catch
        {
            return Errors.Post.EditFailed;
        }

        return new CommandResponse(
            true,
            "Post edited successfully",
            $"/api/posts/{post.Id}");
    }
}