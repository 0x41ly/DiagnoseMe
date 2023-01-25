namespace Auth.Domain.Common;

public static class Pins
{
    public static class Types 
    {
        public static class Email
        {
            public static string Confirm ="Confirm";
            public static string Change = "Change";
        }
        public static class Password
        {
            public static string Reset = "Reset";
        }
    }
}