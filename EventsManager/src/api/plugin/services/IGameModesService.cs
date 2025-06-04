using EventsManager.plugin.models;

namespace EventsManager.api.plugin.services;

public interface IGameModesService
{
    void ApplyMode(string key);
    GameMode? GetActiveMode();
    IEnumerable<string> GetAvailableModeKeys();
}