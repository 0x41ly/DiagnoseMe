using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Commands.DeleteCommentAgreement;

public class DeleteCommentAgreementCommandValidator : AbstractValidator<DeleteCommentAgreementCommand>
{
    public DeleteCommentAgreementCommandValidator()
    {
        RuleFor(x => x.CommentId)
            .NotEmpty()
            .WithMessage("CommentId is required");
        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage("UserId is required");
    }
}