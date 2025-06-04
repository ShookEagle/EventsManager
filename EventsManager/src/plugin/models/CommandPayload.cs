namespace EventsManager.plugin.models;

public class CommandPayload
{
    public string Type { get; set; }
    public string Executor { get; set;  }
    public string Target { get; set; }
    public string? Reason { get; set; }
}