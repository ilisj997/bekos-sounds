using Microsoft.Win32;

namespace Bekos.Sounds;

/// <summary>
/// Represents a Windows system sound event resolved from the registry.
/// Inherits playback capabilities from <see cref="Sound"/>.
/// If the event has no WAV file assigned in the registry, <see cref="Sound.IsValid"/> is <see langword="false"/> and all playback calls are no-ops.
/// </summary>
public class SystemSound : Sound
{
    /// <summary>Gets the Windows sound event name used to look up this sound in the registry (e.g. <c>"DeviceConnect"</c>).</summary>
    public string EventName { get; }

    /// <summary>Gets the WAV file name without extension (e.g. <c>"Windows Hardware Insert"</c>), or <see langword="null"/> if no file is assigned.</summary>
    public string? SoundName { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="SystemSound"/> by resolving the WAV path for the given event name from the registry.
    /// </summary>
    /// <param name="eventName">The Windows sound event name (e.g. <c>"DeviceConnect"</c>).</param>
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
