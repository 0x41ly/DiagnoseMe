

namespace Auth.Application.Authentication.Commands.SignOut;


public class SignOutCommandHandler :
    IRequestHandler<SignOutCommand>
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public SignOutCommandHandler(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }
    public async Task<Unit> Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        await _signInManager.SignOutAsync();
        return new Unit();
    }
}