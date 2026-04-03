using Microsoft.Win32;

namespace Bekos.Sounds;

/// <summary>
/// Provides lazy-loaded static properties for all known Windows system sound events.
/// Each property resolves its WAV path from the registry on first access.
/// </summary>
public static class SystemSounds
{
    // NOTE: Base registry path where Windows stores sound event entries per user.
    private const string RegistryBasePath = @"AppEvents\Schemes\Apps\.Default";

    /// <summary>
    /// Returns all system sound events that have a WAV file assigned on this machine.
    /// </summary>
    public static IEnumerable<SystemSound> GetAll()
    {
        using var baseKey = Registry.CurrentUser.OpenSubKey(RegistryBasePath);
        if (baseKey is null) yield break;

        foreach (var eventName in baseKey.GetSubKeyNames())
        {
            var sound = new SystemSound(eventName);
            if (sound.IsValid)
                yield return sound;
        }
    }

    /// <summary>
    /// Returns true if the given event name has a WAV file assigned on this machine.
    /// </summary>
    public static bool Exists(string eventName)
    {
        using var key = Registry.CurrentUser.OpenSubKey($@"{RegistryBasePath}\{eventName}\.Default");
        var path = key?.GetValue(string.Empty)?.ToString();
        return !string.IsNullOrEmpty(path);
    }


    private static SystemSound? _deviceConnect;
    public static SystemSound DeviceConnect => _deviceConnect ??= new SystemSound("DeviceConnect");

    private static SystemSound? _deviceDisconnect;
    public static SystemSound DeviceDisconnect => _deviceDisconnect ??= new SystemSound("DeviceDisconnect");

    private static SystemSound? _deviceFail;
    public static SystemSound DeviceFail => _deviceFail ??= new SystemSound("DeviceFail");


    private static SystemSound? _beep;
    public static SystemSound Beep => _beep ??= new SystemSound(".Default");

    private static SystemSound? _warning;
    public static SystemSound Warning => _warning ??= new SystemSound("SystemExclamation");

    private static SystemSound? _error;
    public static SystemSound Error => _error ??= new SystemSound("SystemHand");

    private static SystemSound? _asterisk;
    public static SystemSound Asterisk => _asterisk ??= new SystemSound("SystemAsterisk");

    private static SystemSound? _question;
    public static SystemSound Question => _question ??= new SystemSound("SystemQuestion");

    private static SystemSound? _notification;
    public static SystemSound Notification => _notification ??= new SystemSound("SystemNotification");


    private static SystemSound? _printComplete;
    public static SystemSound PrintComplete => _printComplete ??= new SystemSound("PrintComplete");

    private static SystemSound? _menuCommand;
    public static SystemSound MenuCommand => _menuCommand ??= new SystemSound("MenuCommand");

    private static SystemSound? _menuPopup;
    public static SystemSound MenuPopup => _menuPopup ??= new SystemSound("MenuPopup");

    private static SystemSound? _lowBattery;
    public static SystemSound LowBattery => _lowBattery ??= new SystemSound("LowBatteryAlarm");

    private static SystemSound? _criticalBattery;
    public static SystemSound CriticalBattery => _criticalBattery ??= new SystemSound("CriticalBatteryAlarm");

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

    private static SystemSound? _logoff;
    public static SystemSound Logoff => _logoff ??= new SystemSound("WindowsLogoff");

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


    private static SystemSound? _call1;
    public static SystemSound Call1 => _call1 ??= new SystemSound("Notification.Looping.Call");

    private static SystemSound? _call2;
    public static SystemSound Call2 => _call2 ??= new SystemSound("Notification.Looping.Call2");

    private static SystemSound? _call3;
    public static SystemSound Call3 => _call3 ??= new SystemSound("Notification.Looping.Call3");

    private static SystemSound? _call4;
    public static SystemSound Call4 => _call4 ??= new SystemSound("Notification.Looping.Call4");

    private static SystemSound? _call5;
    public static SystemSound Call5 => _call5 ??= new SystemSound("Notification.Looping.Call5");

    private static SystemSound? _call6;
    public static SystemSound Call6 => _call6 ??= new SystemSound("Notification.Looping.Call6");

    private static SystemSound? _call7;
    public static SystemSound Call7 => _call7 ??= new SystemSound("Notification.Looping.Call7");

    private static SystemSound? _call8;
    public static SystemSound Call8 => _call8 ??= new SystemSound("Notification.Looping.Call8");

    private static SystemSound? _call9;
    public static SystemSound Call9 => _call9 ??= new SystemSound("Notification.Looping.Call9");

    private static SystemSound? _call10;
    public static SystemSound Call10 => _call10 ??= new SystemSound("Notification.Looping.Call10");
}
