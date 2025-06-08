using CounterStrikeSharp.API.Core;
using EventsManager.api.plugin;
using EventsManager.api.plugin.services;
using EventsManager.plugin.enums;
using EventsManager.plugin.models;

namespace EventsManager.plugin.services;

public class LoggerService(IEventsManager plugin) : ILoggerService
{
    private readonly IWebService _api = plugin.GetWebService();
    
    private readonly List<LogEntry> _logBuffer = [];
    private const int MaxBufferSize = 200;
    
    private void Log(string message, LogType type = LogType.INFO, Dictionary<string, object>? meta = null)
    {
        meta ??= new Dictionary<string, object> { { "none", "none" } };
        
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
    public void Mode(CCSPlayerController executor, string mode, Dictionary<string, object>? meta = null) =>
    Log($"{executor.PlayerName} changed the mode to {mode}", LogType.MODE, meta); 
    public void Map(CCSPlayerController executor, string map, Dictionary<string, object>? meta = null) =>
        Log($"{executor.PlayerName} changed the map to {map}", LogType.MAP, meta); 
}