using ErrorOr;
using MediatR;
using MedicalBlog.Application.Common.Interfaces.Persistence;
using MedicalBlog.Application.MedicalBlog.Common;
using MedicalBlog.Domain.Common.Errors;

namespace MedicalBlog.Application.MedicalBlog.Commands.UnsubscribeDoctor;

public class UnsubscribeDoctorCommandHandler : IRequestHandler<UnsubscribeDoctorCommand, ErrorOr<CommandResponse>>
{
    private readonly IUserSubscribedUserRepository _userSubscribedUserRepository;
    private readonly IUserRepository _userRepository;
    public UnsubscribeDoctorCommandHandler(
        IUserSubscribedUserRepository userSubscribedUserRepository,
        IUserRepository userRepository)
    {
        _userSubscribedUserRepository = userSubscribedUserRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<CommandResponse>> Handle(UnsubscribeDoctorCommand command, CancellationToken cancellationToken)
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
        var userSubscribedUser = (await _userSubscribedUserRepository
            .Get( x => x.UserId == command.UserId && x.SubscribedUserId == command.DoctorId))
            .FirstOrDefault();
        if (userSubscribedUser == null)
        {
            return Errors.User.YouAreNotSubscribedToThisDoctor;
        }
        try
        {
            _userSubscribedUserRepository.Remove(userSubscribedUser);
            await _userSubscribedUserRepository.Save();
        }
        catch
        {
            return Errors.User.FailedToUnsubscribe;
        }
        return new CommandResponse(
            true,
            $"You have successfully unsubscribed from this doctor with id {command.DoctorId}.",
            null!);
    }
}