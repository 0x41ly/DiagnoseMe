using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Queries.GetPostsByDoctorId;

public class GetPostsByDoctorIdValidator : AbstractValidator<GetPostsByDoctorIdQuery>
{
    public GetPostsByDoctorIdValidator()
    {
        RuleFor(x => x.PageNumber)
            .NotEmpty()
            .WithMessage("Page number cannot be empty.")
            .GreaterThan(0)
            .WithMessage("Page number must be positive.");
        RuleFor(x => x.DoctorId)
            .NotEmpty()
            .WithMessage("Docter id cannot be empty.");
        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage("UserId cannot be null.");
    }
}