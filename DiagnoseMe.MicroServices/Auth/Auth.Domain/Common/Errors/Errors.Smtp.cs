using ErrorOr;

namespace Auth.Domain.Common.Errors;


public partial class Errors
{
    public class Smtp
    {
        public static Error SendFail => Error.Failure(
            code: "Email.SendFail",
            description: "Failed to send email, \n Please Contact the support at support@DiagnoseMe.com");
    }
}
