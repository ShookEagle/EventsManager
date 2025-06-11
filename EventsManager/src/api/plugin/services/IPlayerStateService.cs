using CounterStrikeSharp.API.Core;
using EventsManager.plugin.models;

namespace EventsManager.api.plugin.services;

public interface IPlayerStateService
{
    PlayerState GetOrCreate(CCSPlayerController player);
    bool TryGet(int slot, out PlayerState? state);
    void Remove(int slot);
    IEnumerable<PlayerState> GetAll();
    void SchedulePush();
    Task<bool> PushAsync();
}
