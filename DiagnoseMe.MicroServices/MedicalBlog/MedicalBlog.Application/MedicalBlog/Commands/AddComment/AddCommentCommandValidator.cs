using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Commands.AddComment;

public class AddCommentCommandValidator : AbstractValidator<AddCommentCommand>
{
    public AddCommentCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content is required");

        RuleFor(x => x.AuthorId)
            .NotEmpty()
            .WithMessage("AuthorId is required");
        RuleFor(x => x.PostId)
            .NotEmpty()
            .WithMessage("PostId is required");
    }
}