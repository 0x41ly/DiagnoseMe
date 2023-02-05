using FluentValidation;

namespace MedicalBlog.Application.MedicalBlog.Commands.SubscribeDoctor;

public class SubscribeDoctorCommandValidator : AbstractValidator<SubscribeDoctorCommand>
{
    public SubscribeDoctorCommandValidator()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty()
            .WithMessage("DoctorId is required");
        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage("UserId is required");
    }
}