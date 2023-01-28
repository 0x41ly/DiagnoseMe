using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetCommentsyParentId;

public class GetCommentsByParentIdQueryValidator : AbstractValidator<GetCommentsByParentIdQuery>
{
    public GetCommentsByParentIdQueryValidator()
    {
        RuleFor(x => x.ParentId)
            .NotEmpty()
            .WithMessage("PostId cannot be empty.");
        RuleFor(x => x.PageNumber)
            .NotEmpty()
            .WithMessage("PageNumber cannot be empty.")
            .GreaterThan(0)
            .WithMessage("PageNumber must be positive");
    }
}