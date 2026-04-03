using System.Diagnostics.CodeAnalysis;
using System.Media;

namespace Bekos.Sounds;

public class Sound : IDisposable
{
    public string? Path { get; }

    [MemberNotNullWhen(true, nameof(_soundPlayer))]
    public bool IsValid { get; }

    private readonly SoundPlayer? _soundPlayer;

    public Sound(string? path)
    {
        if (path is null || !File.Exists(path) || !path.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
            return;

        Path = path;
        IsValid = true;

        _soundPlayer = new SoundPlayer(path);
    }

    public void Play()
    {
        if (!IsValid) return;
        _soundPlayer.Play();
    }

    public void PlaySync()
    {
        if (!IsValid) return;
        _soundPlayer.PlaySync();
    }

    public void PlayLooping()
    {
        if (!IsValid) return;
        _soundPlayer.PlayLooping();
    }

    public void Stop() => _soundPlayer?.Stop();

    public void Dispose()
    {
        Stop();
        _soundPlayer?.Dispose();
    }
}
