using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Commands.UnsubscribeDoctor;
 public record UnsubscribeDoctorCommand(
     string DoctorId,
     string UserId) : IRequest<ErrorOr<CommandResponse>>;