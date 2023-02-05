using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Commands.AgreeComment;

public class AgreeCommentCommandValidator : AbstractValidator<AgreeCommentCommand>
{
    public AgreeCommentCommandValidator()
    {
        RuleFor(x => x.CommentId)
            .NotEmpty()
            .WithMessage("CommentId is required");
        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage("UserId is required");
    }
}