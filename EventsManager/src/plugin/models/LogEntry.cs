namespace EventsManager.plugin.models;

public class LogEntry
{
    public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    public string Type { get; set; } = "INFO";
    public string Message { get; set; } = string.Empty;
    public Dictionary<string, object>? Meta { get; set; } = new();
}