using EventsManager.api.plugin;
using EventsManager.api.plugin.services;
using EventsManager.plugin.enums;
using EventsManager.plugin.models;

namespace EventsManager.plugin.services;

public class LoggerService(IEventsManager plugin)
{
    private readonly IWebService _api = plugin.GetWebService();
    
    private readonly List<LogEntry> _logBuffer = [];
    private const int MaxBufferSize = 200;
    
    public void Log(string message, LogType type = LogType.INFO, Dictionary<string, object>? meta = null)
    {
        var entry = new LogEntry
        {
            Type = type.ToString(),
            Message = message,
            Meta = meta
        };

        _ = _api.PostAsync("logs.php", entry);
    }
    
    public void Info(string message, Dictionary<string, object>? meta = null) => Log(message, LogType.INFO, meta);
    public void Warn(string message, Dictionary<string, object>? meta = null) => Log(message, LogType.WARN, meta);
    public void Error(string message, Dictionary<string, object>? meta = null) => Log(message, LogType.ERROR, meta);
    public void Action(string message, Dictionary<string, object>? meta = null) => Log(message, LogType.ACTION, meta);
}