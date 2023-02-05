using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetPostsByTags;

public class GetPostsByTagsQueryValidator : AbstractValidator<GetPostsByTagsQuery>
{
    public GetPostsByTagsQueryValidator()
    {
        RuleFor(x => x.PageNumber).NotEmpty()
            .WithMessage("Page number cannot be empty.")
            .GreaterThan(0)
            .WithMessage("Page number must be positive.");
        RuleFor(x => x.Tags).NotEmpty()
            .WithMessage("Tags cannot be empty.")
            .Must(x => x.Count >= 1)
            .WithMessage("List of tags must contain at least one tag.");
        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage("UserId cannot be null.");
    }
}