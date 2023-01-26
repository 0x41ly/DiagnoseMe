using FluentValidation;
namespace MedicalBlog.Application.MedicalBlog.Queries.GetCommentsByPostId;
public class GetCommentsByPostIdQueryValidator : AbstractValidator<GetCommentsByPostIdQuery>
{
    public GetCommentsByPostIdQueryValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty()
            .WithMessage("PostId cannot be empty.");
        RuleFor(x => x.PageNumber)
            .NotEmpty()
            .WithMessage("PageNumber cannot be empty.")
            .GreaterThan(0)
            .WithMessage("PageNumber must be positive");
    }
}