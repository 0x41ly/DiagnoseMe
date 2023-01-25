namespace Auth.Application.Settings;

public  class MailSettings
{
    public  string Mail { get; init; }  = null!;
    public  string DisplayName { get; init;} = null!;
    public  string Password { get; init; } = null!;
    public  string Host { get; init;}  = null!;
    public  int Port { get; init;}
}