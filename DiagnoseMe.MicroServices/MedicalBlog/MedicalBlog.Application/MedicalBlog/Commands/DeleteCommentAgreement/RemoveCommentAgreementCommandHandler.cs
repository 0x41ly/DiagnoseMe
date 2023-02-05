using ErrorOr;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Commands.AgreeComment;
using MedicalBlog.Application.MedicalBlog.Common;
using MedicalBlog.Domain.Common.Errors;

namespace MedicalBlog.Application.MedicalBlog.Commands.DeleteCommentAgreement;

public class DeleteCommentAgreementCommandHandler : IRequestHandler<DeleteCommentAgreementCommand, ErrorOr<CommandResponse>>
{
    private readonly ICommentAgreementRepository _commentAgreementRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;
    public DeleteCommentAgreementCommandHandler(
        ICommentAgreementRepository commentAgreementRepository,
        ICommentRepository commentRepository,
        IUserRepository userRepository,
        IMediator mediator)
    {
        _commentAgreementRepository = commentAgreementRepository;
        _commentRepository = commentRepository;
        _userRepository = userRepository;
        _mediator = mediator;
    }
    public async Task<ErrorOr<CommandResponse>> Handle(DeleteCommentAgreementCommand command, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(command.CommentId);
        if (comment == null)
        {
            return Errors.Comment.NotFound;
        }
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user == null)
        {
            // TODO: Check for user in Auth service
            return Errors.User.NotFound;
        }
        var commentAgreement = (await _commentAgreementRepository.Get(
            x => x.CommentId == command.CommentId && x.UserId == command.UserId))
            .FirstOrDefault();
        if (commentAgreement == null)
        {
            return await _mediator.Send(
                new AgreeCommentCommand(
                    command.CommentId, 
                    command.UserId));
        }
        else
        {
             _commentAgreementRepository.Remove(commentAgreement);
            await _commentAgreementRepository.Save();
            return new CommandResponse(
                true,
                "Comment agreement removed successfully",
                ""
            );
        }
    }
}