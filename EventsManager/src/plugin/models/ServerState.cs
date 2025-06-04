namespace EventsManager.plugin.models;

public class ServerState
{
    public string Mode { get; set; } = "Default";
    public string Map { get; set; } = "Active";
    public long UpdatedAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
}