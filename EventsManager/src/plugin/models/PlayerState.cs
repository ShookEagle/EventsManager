using CounterStrikeSharp.API;
using EventsManager.plugin.extensions;

namespace EventsManager.plugin.models;
public class PlayerState(ulong steamId, string name)
{
    public ulong SteamId { get; init; } = steamId;
    public string Name { get; set; } = name;
    public string Team { get; set; } = Utilities.GetPlayerFromSteamId(steamId)!.Team.ToString();
    public string Rank { get; set; } = Utilities.GetPlayerFromSteamId(steamId)!.GetRank().ToString();
    public bool Bot { get; set; } = Utilities.GetPlayerFromSteamId(steamId)!.IsBot;
    public long JoinTime { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
}