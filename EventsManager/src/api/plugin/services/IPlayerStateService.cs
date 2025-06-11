using CounterStrikeSharp.API.Core;
using EventsManager.plugin.models;

namespace EventsManager.api.plugin.services;

public interface IPlayerStateService
{
    PlayerState GetOrCreate(CCSPlayerController player);
    bool TryGet(ulong steamId, out PlayerState? state);
    void Remove(ulong steamId);
    IEnumerable<PlayerState> GetAll();
    Task<bool> PushAsync();
}
