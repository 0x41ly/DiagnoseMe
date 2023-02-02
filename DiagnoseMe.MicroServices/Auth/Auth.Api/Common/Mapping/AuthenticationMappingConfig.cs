using Auth.Application.Authentication.Commands.AddUserToRole;
using Auth.Application.Authentication.Commands.ChangeEmail;
using Auth.Application.Authentication.Commands.ChangeName;
using Auth.Application.Authentication.Commands.ChangePassword;
using Auth.Application.Authentication.Commands.Register;
using Auth.Application.Authentication.Commands.RemoveUserFromRole;
using Auth.Application.Authentication.Commands.UploadProfilePicture;
using Auth.Contracts.Authentication;
using Auth.Domain.Entities;
using Mapster;

namespace Auth.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(UploadProfilePictureRequest request, string username),UploadProfilePictureCommand>().
                    Map(dest => dest.UserName, src => src.username).
                    Map(dest => dest, src => src.request);
        config.NewConfig<(RemoveUserToRoleRequest request, string role), RemoveUserFromRoleCommand>().
                        Map(dest => dest.Role , src => src.role).
                        Map(dest => dest, src => src.request);
        config.NewConfig<(AddUserToRoleRequest request, string role), AddUserToRoleCommand>().
                        Map(dest => dest.Role , src => src.role).
                        Map(dest => dest, src => src.request);
        config.NewConfig<(ChangeNameRequest request, string username),ChangeNameCommand>().
                    Map(dest => dest.UserName, src => src.username).
                    Map(dest => dest, src => src.request);
        config.NewConfig<(ChangeEmailRequest request, string username),ChangeEmailCommand>().
                    Map(dest => dest.UserName, src => src.username).
                    Map(dest => dest, src => src.request);
        config.NewConfig<RegisterRequest,RegisterCommand>().
                    Map(dest => dest.Password, src => src.Password).
                    Map(dest => dest.DateOfBirth, src => src.DateOfbirth).
                    Map(dest => dest.User, src => src);
        config.NewConfig<ApplicationUser,ApplicationUserResponse>().
                    Map(dest => dest.FullName , src => $"{src.FirstName} {src.LastName}").
                    Map(dest => dest.DateOfBirth, src => src.DateOfBirth.ToString()).
                    Map(dest => dest, src => src);
        config.NewConfig<(ChangePasswordRequest request,string username), ChangePasswordCommand>()
                    .Map(dest => dest.NewPassword, src => src.request.NewPassword)
                    .Map(dest => dest.OldPassword, src => src.request.OldPassword)
                    .Map(dest => dest.UserName, src => src.username);
    }
}