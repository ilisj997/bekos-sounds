using Microsoft.Win32;

namespace Bekos.Sounds;

public class SystemSound : Sound
{
    public string EventName { get; }
    public string? SoundName { get; }

    public SystemSound(string eventName) : base(ResolvePathFromRegistry(eventName))
    {
        EventName = eventName;

        if (Path is not null)
            SoundName = System.IO.Path.GetFileNameWithoutExtension(Path);
    }

    private static string? ResolvePathFromRegistry(string eventName)
    {
        // NOTE: Windows stores sound event file paths under this registry key per user.
        var key = $@"AppEvents\Schemes\Apps\.Default\{eventName}\.Default";
        using var reg = Registry.CurrentUser.OpenSubKey(key);
        return reg?.GetValue(string.Empty)?.ToString();
    }
}
