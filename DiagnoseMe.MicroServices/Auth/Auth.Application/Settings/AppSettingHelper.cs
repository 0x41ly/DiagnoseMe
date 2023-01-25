

using Microsoft.Extensions.Configuration;

namespace Auth.Application.Settings;

public static class AppSettingHelper
{
    private static IConfiguration? _config;
    public static void AppSettingHelperConfigure(IConfiguration config){
        _config=config;
    }

    public static string Setting(string Key){
        return _config![Key]!;
    }
}
