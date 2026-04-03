using Microsoft.Win32;
using System.Media;

namespace Bekos.Sounds;

public class SystemSound : IDisposable
{
    public string? SystemName { get; }
    public string? Path { get; }
    public bool IsValid { get; }

    public SystemSound(string systemName)
    {
        // NOTE: Windows stores sound event file paths under this registry key per user.
        var key = $@"AppEvents\Schemes\Apps\.Default\{systemName}\.Default";
        using var reg = Registry.CurrentUser.OpenSubKey(key);

        Path = reg?.GetValue(string.Empty)?.ToString();
        if (Path is null) return;

        SystemName = System.IO.Path.GetFileNameWithoutExtension(Path);

        IsValid = true;
    }

    private SoundPlayer? _soundPlayer;
    public void Play()
    {
        if (!IsValid) return;

        _soundPlayer ??= new SoundPlayer(Path!);
        _soundPlayer.Play();
    }

    public void PlayLooping()
    {
        if (!IsValid) return;

        _soundPlayer ??= new SoundPlayer(Path!);
        _soundPlayer.PlayLooping();
    }

    public void PlaySync()
    {
        if (!IsValid) return;

        _soundPlayer ??= new SoundPlayer(Path!);
        _soundPlayer.PlaySync();
    }

    public void Stop() => _soundPlayer?.Stop();

    public void Dispose()
    {
        Stop();
        _soundPlayer?.Dispose();
        _soundPlayer = null;
    }
}
