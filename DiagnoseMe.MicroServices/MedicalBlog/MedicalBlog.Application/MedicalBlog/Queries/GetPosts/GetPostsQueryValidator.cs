using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetPosts;

public class GetPostsQueryValidator : AbstractValidator<GetPostsQuery>
{
    public GetPostsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .NotEmpty()
            .WithMessage("Page number cannot be empty.")
            .GreaterThan(0)
            .WithMessage("Page number must be positive.");
        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage("UserId cannot be null.");
    }
}