using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Commands.ReplyComment
{
    public class ReplyToCommentCommandValidator : AbstractValidator<ReplyToCommentCommand>
    {
        public ReplyToCommentCommandValidator()
        {
            RuleFor(x => x.PostId)
                .NotEmpty()
                .WithMessage("PostId is required");
            RuleFor(x => x.ParentId)
                .NotEmpty()
                .WithMessage("ParentId is required");
            RuleFor(x => x.AuthorId)
                .NotEmpty()
                .WithMessage("AuthorId is required");
            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Content is required");
        }
    }
}