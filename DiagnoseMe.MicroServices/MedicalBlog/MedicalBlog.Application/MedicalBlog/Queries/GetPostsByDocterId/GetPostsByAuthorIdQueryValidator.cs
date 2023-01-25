using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetPostsByAuthorId;

public class GetPostsByAuthorIdValidator : AbstractValidator<GetPostsByAuthorIdQuery>
{
    public GetPostsByAuthorIdValidator()
    {
        RuleFor(x => x.PageNumber)
            .NotEmpty()
            .WithMessage("Page number cannot be empty.")
            .GreaterThan(0)
            .WithMessage("Page number must be positive.");
        RuleFor(x => x.AuthorId)
            .NotEmpty()
            .WithMessage("Docter id cannot be empty.");
    }
}