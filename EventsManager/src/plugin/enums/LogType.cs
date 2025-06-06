using Serilog;

namespace EventsManager.plugin.enums;

public enum LogType
{
    INFO    = 1,
    WARN    = 2,
    ERROR   = 3,
    ACTION  = 4,
    MODE    = 5,
    MAP     = 6,
}

public static class LogTypeExtensions {
    public static string ToString(this LogType type) {
        return type switch {
            LogType.INFO    => "INFO",
            LogType.WARN    => "WARN",
            LogType.ERROR   => "ERROR",
            LogType.ACTION  => "ACTION",
            LogType.MODE    => "MODE",
            LogType.MAP     => "MAP",
            _               => "INFO"
        };
    }
}