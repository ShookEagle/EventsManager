using CounterStrikeSharp.API.Core;
using EventsManager.api.plugin.services;
using EventsManager.plugin.models;
using Microsoft.Extensions.Logging;

namespace EventsManager.plugin.services;

public class PlayerStateService(IWebService api, EventsManager plugin) : IPlayerStateService
{
    private readonly Dictionary<int, PlayerState> _playerStates = new();
    private readonly SemaphoreSlim _pushLock = new(1, 1);
    private bool _pendingPush = false;
    
    public PlayerState GetOrCreate(CCSPlayerController player)
    {
        if (_playerStates.TryGetValue(player.Slot, out var state)) return state;
        state = new PlayerState(player);
        _playerStates[player.Slot] = state;

        return state;
    }

    public bool TryGet(int slot, out PlayerState? state) => _playerStates.TryGetValue(slot, out state);
    public void Remove(int slot) => _playerStates.Remove(slot);
    public IEnumerable<PlayerState> GetAll() => _playerStates.Values;

    public void SchedulePush()
    {
        if (_pendingPush) return;
        _pendingPush = true;

        Task.Run(async () =>
        {
            await Task.Delay(1000); // debounce window
            _pendingPush = false;
            await PushAsync();
        });
    }
    public async Task<bool> PushAsync()
    {
        await _pushLock.WaitAsync();
        try
        {
            var dict = _playerStates.ToDictionary(p => p.Key.ToString(), p => p.Value);
            var success = await api.PostAsync("players.php", dict);
            plugin.Logger.LogInformation(success
                ? "[PLAYER STATE] Pushed successfully."
                : "[PLAYER STATE] Push failed.");
            return success;
        }
        finally
        {
            _pushLock.Release();
        }
    }
}