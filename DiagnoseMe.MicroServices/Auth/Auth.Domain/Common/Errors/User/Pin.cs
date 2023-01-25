using ErrorOr;

namespace Auth.Domain.Common.Errors;
public static partial class Errors
{
    public static partial class User
    {
        public static class Pin
        {
            public static Error Invalid => Error.Validation(
                code: "User.Pin.Invalid",
                description: "Pin code is invalid");
            public static Error Expired => Error.Validation(
                code: "User.Pin.Expired",
                description: "Pin code is Expired"); 
            public static class Id
            {
                public static Error Null => Error.Validation(
                    code: "User.Pin.Id.Null",
                    description: "Id field can not be null"
                );
            }
            public static class PinCode
            {
                public static Error Null => Error.Validation(
                    code: "User.Pin.PinCode.Null",
                    description: "PinCode field can not be null"
                );
            }
        }
    }
}