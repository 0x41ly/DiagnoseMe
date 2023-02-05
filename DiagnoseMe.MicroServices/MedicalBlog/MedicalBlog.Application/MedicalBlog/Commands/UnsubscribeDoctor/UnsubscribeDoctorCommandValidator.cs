using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Commands.UnsubscribeDoctor;

public class UnsubscribeDoctorCommandValidator : AbstractValidator<UnsubscribeDoctorCommand>
{
    public UnsubscribeDoctorCommandValidator()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty()
            .WithMessage("DoctorId is required");
        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage("UserId is required");
    }
}