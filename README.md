# Bekos.Sounds

**Bekos.Sounds** is a Windows-only .NET library written in C#.
It provides a unified API for playing Windows system sound events and arbitrary WAV files.

> ⚠️ **Platform:** This library targets Windows only. It uses `Microsoft.Win32.Registry` and `System.Media.SoundPlayer`, neither of which are available on Linux or macOS.

## Features
- **Lazy-loaded system sounds** — All built-in Windows sound events (device connect/disconnect, warnings, alarms, ringtones, notifications, etc.) are exposed as static properties and initialized on first access.
- **Play any WAV file** — The `Sound` class plays any `.wav` file by path, independent of the system sound registry.
- **Multiple playback modes** — Supports fire-and-forget (`Play`), synchronous (`PlaySync`), and looping (`PlayLooping`) playback.
- **Registry-based resolution** — Resolves each system sound's `.wav` path directly from `HKCU\AppEvents\Schemes`, so it always reflects the user's current sound theme.
- **Registry enumeration** — `GetAll()` returns all sound events that have a WAV file assigned on the current machine.
- **Event existence check** — `Exists()` checks whether a given sound event has a WAV file assigned without creating an instance.
- **Safe fallback** — If a sound event has no file assigned in the registry, `IsValid` is `false` and all playback calls are no-ops.
- **Disposable** — Both `Sound` and `SystemSound` implement `IDisposable` to release the underlying `SoundPlayer`.

## Details
- Written in **C#**.
- Windows only — depends on `Microsoft.Win32.Registry` and `System.Media.SoundPlayer`.
- Uses `UseWindowsForms` to bring in `System.Media` without an additional NuGet package.

## Usage

```csharp
using Bekos.Sounds;

// Play a system sound (non-blocking)
SystemSounds.Warning.Play();

// Play and wait until finished
SystemSounds.DeviceConnect.PlaySync();

// Loop until stopped
SystemSounds.Alarm1.PlayLooping();
SystemSounds.Alarm1.Stop();

// Enumerate all system sounds available on this machine
foreach (var s in SystemSounds.GetAll())
    Console.WriteLine($"{s.EventName} → {s.SoundName}");

// Dispose when done
SystemSounds.Error.Dispose();

// Play any WAV file by path
using var sound = new Sound(@"C:\sounds\alert.wav");
if (sound.IsValid)
    sound.Play();
```
