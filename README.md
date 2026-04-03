# Sounds

**Sounds** is a Windows-only .NET library written in C#.
It provides strongly-typed access to Windows system sound events, resolving each sound's file path from the registry and playing it via `SoundPlayer`.

> ⚠️ **Platform:** This library targets Windows only. It uses `Microsoft.Win32.Registry` and `System.Media.SoundPlayer`, neither of which are available on Linux or macOS.

## Features
- **Lazy-loaded system sounds** — All built-in Windows sound events (device connect/disconnect, warnings, alarms, ringtones, notifications, etc.) are exposed as static properties and initialized on first access.
- **Multiple playback modes** — Supports fire-and-forget (`Play`), synchronous (`PlaySync`), and looping (`PlayLooping`) playback.
- **Registry-based resolution** — Resolves each sound's `.wav` path directly from `HKCU\AppEvents\Schemes`, so it always reflects the user's current sound theme.
- **Safe fallback** — If a sound event has no file assigned in the registry, `IsValid` is `false` and all play calls are no-ops.
- **Disposable** — `SystemSound` implements `IDisposable` to release the underlying `SoundPlayer`.

## Details
- Written in **C#** targeting **.NET 10**.
- Windows only — depends on `Microsoft.Win32.Registry` and `System.Media.SoundPlayer`.

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

// Dispose when done
SystemSounds.Error.Dispose();
```
