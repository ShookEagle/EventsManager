using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using EventsManager.plugin.extensions;

namespace EventsManager.plugin.models;
public class PlayerState(CCSPlayerController player)
{
    public ulong SteamId { get; init; } = player.SteamID;
    public string Name { get; set; } = player.PlayerName;
    public string Team { get; set; } = player.Team.ToString() ;
    public string Rank { get; set; } = player.GetRank().ToString();
    public bool Bot { get; set; } = player.IsBot;
    public long JoinTime { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
}