using ErrorOr;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;
using MedicalBlog.Domain.Common.Errors;

namespace MedicalBlog.Application.MedicalBlog.Commands.RatePost;


public class RatePostCommandHandler : IRequestHandler<RatePostCommand, ErrorOr<CommandResponse>>
{
    private readonly IPostRatingRepository _postRatingRepository;
    private readonly IPostRepository _postRepository;
    private readonly IPostViewRepository _postViewRepository;
    private readonly IUserRepository _userRepository;

    public RatePostCommandHandler(
        IPostRatingRepository postRatingRepository,
        IPostRepository postRepository,
        IPostViewRepository postViewRepository,
        IUserRepository userRepository)
    {
        _postRatingRepository = postRatingRepository;
        _postRepository = postRepository;
        _postViewRepository = postViewRepository;
        _userRepository = userRepository;
    }


    public async Task<ErrorOr<CommandResponse>> Handle(RatePostCommand command, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(command.PostId);
        if (post == null)
        {
            return Errors.Post.NotFound;
        }
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user == null)
        {
            // TODO: Check for user in Auth service
            return Errors.User.NotFound;
        }
        var postRating = (await _postRatingRepository.Get(
            x => x.PostId == command.PostId && x.UserId == command.UserId))
            .FirstOrDefault();

        try
        {   
            if (postRating == null)
            {
                postRating = new PostRating{
                    PostId = command.PostId,
                    UserId = command.UserId,
                    Rating = command.Rating
                };
                await _postRatingRepository.AddAsync(postRating);
            }
            else{
                postRating.Rating = command.Rating;
                await _postRatingRepository.Edit(postRating);
            }
            await _postRatingRepository.Save();
        }catch{
            return Errors.Post.RatingFailed;
        }
        return new CommandResponse(
            true,
            "Post rated successfully",
            $"/api/posts/{command.PostId}"
        );
    }
}