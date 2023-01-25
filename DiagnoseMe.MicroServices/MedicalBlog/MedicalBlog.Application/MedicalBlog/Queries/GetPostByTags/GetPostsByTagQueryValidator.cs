using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetPostByTags;

public class GetPostsByTagQueryValidator : AbstractValidator<GetPostsByTagQuery>
{
    public GetPostsByTagQueryValidator()
    {
        RuleFor(x => x.PageNumber).NotEmpty()
            .WithMessage("Page number cannot be empty.")
            .GreaterThan(0)
            .WithMessage("Page number must be positive.");
        RuleFor(x => x.Tags).NotEmpty()
            .WithMessage("Tags cannot be empty.")
            .Must(x => x.Count >= 1)
            .WithMessage("List of tags must contain at least one tag.");
    }
}