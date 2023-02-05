using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetPost;


public class GetPostQueryValidator : AbstractValidator<GetPostByIdQuery>
{
    public GetPostQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty.");
        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage("UserId cannot be null.");
    }
}