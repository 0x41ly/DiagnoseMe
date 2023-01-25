using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetPost;


public class GetPostQueryValidator : AbstractValidator<GetPostQuery>
{
    public GetPostQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty.");
    }
}