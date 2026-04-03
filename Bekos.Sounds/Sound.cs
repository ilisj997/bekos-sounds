using System.Diagnostics.CodeAnalysis;
using System.Media;

namespace Bekos.Sounds;

/// <summary>
/// Plays a WAV file from a given file path.
/// If the path is null, does not exist, or is not a WAV file, the instance is created in an invalid state and all playback calls are no-ops.
/// </summary>
public class Sound : IDisposable
{
    /// <summary>Gets the absolute path to the WAV file, or <see langword="null"/> if the sound is invalid.</summary>
    public string? Path { get; }

    /// <summary>Gets a value indicating whether the sound is valid and ready for playback.</summary>
    [MemberNotNullWhen(true, nameof(_soundPlayer))]
    public bool IsValid { get; }

    private readonly SoundPlayer? _soundPlayer;

    /// <summary>
    /// Initializes a new instance of <see cref="Sound"/>.
    /// </summary>
    /// <param name="path">Absolute or relative path to a WAV file. If <see langword="null"/>, missing, or not a WAV file, <see cref="IsValid"/> will be <see langword="false"/>.</param>
    public Sound(string? path)
    {
        if (path is null || !File.Exists(path) || !path.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
            return;

        Path = path;
        IsValid = true;

        _soundPlayer = new SoundPlayer(path);
    }

    /// <summary>Plays the sound asynchronously. Returns immediately. No-op if <see cref="IsValid"/> is <see langword="false"/>.</summary>
    public void Play()
    {
        if (!IsValid) return;
        _soundPlayer.Play();
    }

    /// <summary>Plays the sound and blocks until playback completes. No-op if <see cref="IsValid"/> is <see langword="false"/>.</summary>
    public void PlaySync()
    {
        if (!IsValid) return;
        _soundPlayer.PlaySync();
    }

    /// <summary>Plays the sound in a continuous loop until <see cref="Stop"/> is called. No-op if <see cref="IsValid"/> is <see langword="false"/>.</summary>
    public void PlayLooping()
    {
        if (!IsValid) return;
        _soundPlayer.PlayLooping();
    }

    /// <summary>Stops playback if the sound is currently playing.</summary>
    public void Stop() => _soundPlayer?.Stop();

    /// <summary>Stops playback and releases the underlying <see cref="SoundPlayer"/>.</summary>
    public void Dispose()
    {
        Stop();
        _soundPlayer?.Dispose();
    }
}
