using CounterStrikeSharp.API.Core;

namespace EventsManager.plugin.models;

public abstract class PlayerState
{
    public string Name { get; set; } = "";
    public string Team { get; set; } = "spectator";
    public string Rank { get; set; } = "";
    public bool Muted { get; set; } = false;
    public long JoinTime { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
}