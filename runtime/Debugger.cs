using Godot;

namespace GT.Tweening;

public static class Debugger
{
    private static int _logPriority;

    private const string _LogPrefix = "[color=#0099bc][b]GOTWEEN ► [/b][/color]";

    public static int logPriority => _logPriority;

    public static void Log(object message)
    {
        var str = _LogPrefix + message;
        // TODO:
    }

    public static void LogError(object message, Tween t = null)
    {
        var str = !GOTween.debugMode ? _LogPrefix + message?.ToString() : $"{_LogPrefix}{Debugger.GetDebugDataMessage(t)}{message?.ToString()}";
        if (GOTween.onWillLog != null && !GOTween.onWillLog((LogType)0, str))
            return;
        GD.PushError(str);
    }

    /// <summary>
    /// * 安全模式中捕获错误的日志
    /// </summary>
    /// <param name="message"></param>
    /// <param name="t"></param>
    public static void LogSafeModeCapturedError(object message, Tween t = null)
    {
        var str = !GOTween.debugMode
            ? (_LogPrefix + message)
            : $"{_LogPrefix}{Debugger.GetDebugDataMessage(t)}{message}";
        // TODO:
    }

    /// <summary>
    /// * 安全模式中是否捕捉错误Error
    /// </summary>
    /// <returns></returns>
    public static bool ShouldLogSafeModeCapturedError()
    {
        switch (GOTween.safeModeLogBehaviour)
        {
            case SafeModeLogBehaviour.None:
                return false;
            case SafeModeLogBehaviour.Normal:
            case SafeModeLogBehaviour.Warning:
                return _logPriority >= 1;
            default:
                return true;
        }
    }

    private static string GetDebugDataMessage(Tween t)
    {
        var message = "";
        AddDebugDataToMessage(ref message, t);
        return message;
    }

    private static void AddDebugDataToMessage(ref string message, Tween t)
    {
        // TODO:
    }

    // TODO:
}