using CounterStrikeSharp.API.Core;
using EventsManager.api.plugin.services;
using EventsManager.plugin.models;
using Microsoft.Extensions.Logging;

namespace EventsManager.plugin.services;

public class PlayerStateService(IWebService api, EventsManager plugin) : IPlayerStateService
{
    private readonly Dictionary<ulong, PlayerState> _playerStates = new();
    
    public PlayerState GetOrCreate(CCSPlayerController player)
    {
        if (_playerStates.TryGetValue(player.SteamID, out var state)) return state;
        state = new PlayerState(player.SteamID, player.PlayerName);
        _playerStates[player.SteamID] = state;

        return state;
    }

    public bool TryGet(ulong steamId, out PlayerState? state) => _playerStates.TryGetValue(steamId, out state);
    public void Remove(ulong steamId) => _playerStates.Remove(steamId);
    public IEnumerable<PlayerState> GetAll() => _playerStates.Values;

    public async Task<bool> PushAsync()
    {
        var dict = _playerStates.ToDictionary(p => p.Key.ToString(), p => p.Value);
        var success = await api.PostAsync("players.php", dict);
        if (!success)
            plugin.Logger.LogWarning("[PLAYER STATE] Failed to push player state.");
        else
            plugin.Logger.LogInformation("[PLAYER STATE] Successfully pushed.");
        return success;
    }
}