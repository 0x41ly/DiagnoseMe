namespace Core.Shared.Settings;

public  class MailSettings
{
    public string Mail { get; set; } = string.Empty;
    public static string DisplayName { get; } = "Diagnose Me";
    public string Password { get; set; } = string.Empty;
    public static string Host { get; } = "smtp.gmail.com";
    public static int Port { get; } = 587;
}