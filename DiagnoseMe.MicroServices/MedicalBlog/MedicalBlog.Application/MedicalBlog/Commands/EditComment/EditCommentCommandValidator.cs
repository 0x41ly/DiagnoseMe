using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Commands.EditComment;


public class EditCommentCommandValidator : AbstractValidator<EditCommentCommand>
{
    public EditCommentCommandValidator()
    {
        RuleFor(x => x.CommentId)
            .NotEmpty()
            .WithMessage("CommentId is required");
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required");
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content is required");
    }
}