using ErrorOr;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;
using MedicalBlog.Domain.Common.Errors;

namespace MedicalBlog.Application.MedicalBlog.Commands.SubscribeDoctor;

public class SubscribeDoctorCommandHandler : IRequestHandler<SubscribeDoctorCommand, ErrorOr<CommandResponse>>
{
    private readonly IUserSubscribedUserRepository _userSubscribedUserRepository;
    private readonly IUserRepository _userRepository;
    public SubscribeDoctorCommandHandler(
        IUserSubscribedUserRepository userSubscribedUserRepository,
        IUserRepository userRepository)
    {
        _userSubscribedUserRepository = userSubscribedUserRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<CommandResponse>> Handle(SubscribeDoctorCommand command, CancellationToken cancellationToken)
    {
        var doctor = await _userRepository.GetByIdAsync(command.DoctorId);
        if (doctor == null)
        {
            // Todo: check for user in Auth service
            return Errors.User.DoctorNotFound;
        }
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user == null)
        {
            // Todo: check for user in Auth service
            return Errors.User.NotFound;
        }
        if (doctor.Id == user.Id)
        {
            return Errors.User.YouCanNotDoThis;
        }
        var userSubscribedUser = new UserSubscribedUser
        {
            UserId = command.UserId,
            SubscribedUserId = command.DoctorId
        };
        try
        {
            await _userSubscribedUserRepository.AddAsync(userSubscribedUser);
            await _userSubscribedUserRepository.Save();
        }
        catch
        {
            return Errors.User.FailedToSubscribe;
        }
        return new CommandResponse(
            true,
            $"You have successfully subscribed to this doctor with id {command.DoctorId}.",
            null!);

    }
}