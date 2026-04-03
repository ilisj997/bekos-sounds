using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;




namespace Bekos.Sounds;

public static class SystemSounds
{
    private static SystemSound? _deviceConnect;
    public static SystemSound DeviceConnect => _deviceConnect ??= new SystemSound("DeviceConnect");

    private static SystemSound? _deviceDisconnect;
    public static SystemSound DeviceDisconnect => _deviceDisconnect ??= new SystemSound("DeviceDisconnect");

    private static SystemSound? _deviceFail;
    public static SystemSound DeviceFail => _deviceFail ??= new SystemSound("DeviceFail");


    private static SystemSound? _warning;
    public static SystemSound Warning => _warning ??= new SystemSound("SystemExclamation");

    private static SystemSound? _error;
    public static SystemSound Error => _error ??= new SystemSound("SystemHand");


    private static SystemSound? _mail;
    public static SystemSound Mail => _mail ??= new SystemSound("MailBeep");

    private static SystemSound? _messageNudge;
    public static SystemSound MessageNudge => _messageNudge ??= new SystemSound("MessageNudge");

    private static SystemSound? _default;
    public static SystemSound Default => _default ??= new SystemSound("Notification.Default");

    private static SystemSound? _proximity;
    public static SystemSound Proximity => _proximity ??= new SystemSound("Notification.Proximity");

    private static SystemSound? _proximityConnection;
    public static SystemSound ProximityConnection => _proximityConnection ??= new SystemSound("ProximityConnection");

    private static SystemSound? _reminder;
    public static SystemSound Reminder => _reminder ??= new SystemSound("Notification.Reminder");

    private static SystemSound? _message;
    public static SystemSound Message => _message ??= new SystemSound("Notification.SMS");

    private static SystemSound? _logon;
    public static SystemSound Logon => _logon ??= new SystemSound("WindowsLogon");

    private static SystemSound? _accountControl;
    public static SystemSound AccountControl => _accountControl ??= new SystemSound("WindowsUAC");

    private static SystemSound? _unlock;
    public static SystemSound Unlock => _unlock ??= new SystemSound("WindowsUnlock");


    private static SystemSound? _alarm1;
    public static SystemSound Alarm1 => _alarm1 ??= new SystemSound("Notification.Looping.Alarm");

    private static SystemSound? _alarm2;
    public static SystemSound Alarm2 => _alarm2 ??= new SystemSound("Notification.Looping.Alarm2");

    private static SystemSound? _alarm3;
    public static SystemSound Alarm3 => _alarm3 ??= new SystemSound("Notification.Looping.Alarm3");

    private static SystemSound? _alarm4;
    public static SystemSound Alarm4 => _alarm4 ??= new SystemSound("Notification.Looping.Alarm4");

    private static SystemSound? _alarm5;
    public static SystemSound Alarm5 => _alarm5 ??= new SystemSound("Notification.Looping.Alarm5");

    private static SystemSound? _alarm6;
    public static SystemSound Alarm6 => _alarm6 ??= new SystemSound("Notification.Looping.Alarm6");

    private static SystemSound? _alarm7;
    public static SystemSound Alarm7 => _alarm7 ??= new SystemSound("Notification.Looping.Alarm7");

    private static SystemSound? _alarm8;
    public static SystemSound Alarm8 => _alarm8 ??= new SystemSound("Notification.Looping.Alarm8");

    private static SystemSound? _alarm9;
    public static SystemSound Alarm9 => _alarm9 ??= new SystemSound("Notification.Looping.Alarm9");

    private static SystemSound? _alarm10;
    public static SystemSound Alarm10 => _alarm10 ??= new SystemSound("Notification.Looping.Alarm10");


    private static SystemSound? _ring1;
    public static SystemSound Ring1 => _ring1 ??= new SystemSound("Notification.Looping.Call");

    private static SystemSound? _ring2;
    public static SystemSound Ring2 => _ring2 ??= new SystemSound("Notification.Looping.Call2");

    private static SystemSound? _ring3;
    public static SystemSound Ring3 => _ring3 ??= new SystemSound("Notification.Looping.Call3");

    private static SystemSound? _ring4;
    public static SystemSound Ring4 => _ring4 ??= new SystemSound("Notification.Looping.Call4");

    private static SystemSound? _ring5;
    public static SystemSound Ring5 => _ring5 ??= new SystemSound("Notification.Looping.Call5");

    private static SystemSound? _ring6;
    public static SystemSound Ring6 => _ring6 ??= new SystemSound("Notification.Looping.Call6");

    private static SystemSound? _ring7;
    public static SystemSound Ring7 => _ring7 ??= new SystemSound("Notification.Looping.Call7");

    private static SystemSound? _ring8;
    public static SystemSound Ring8 => _ring8 ??= new SystemSound("Notification.Looping.Call8");

    private static SystemSound? _ring9;
    public static SystemSound Ring9 => _ring9 ??= new SystemSound("Notification.Looping.Call9");

    private static SystemSound? _ring10;
    public static SystemSound Ring10 => _ring10 ??= new SystemSound("Notification.Looping.Call10");
}

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

        string fileName = new FileInfo(Path).Name;
        // Strip the file extension to get the bare sound name.
        SystemName = fileName[..fileName.LastIndexOf(".")];

        IsValid = true;
    } 

    private SoundPlayer? _soundPlayer;
    public void Play()
    {
        if (!IsValid) return;

        _soundPlayer ??= new SoundPlayer(Path!);
        _soundPlayer?.Play();
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
