using EventsManager.plugin.models;

namespace EventsManager.api.plugin.services;

public interface IGameModeService
{
    Task<bool> LoadAsync();
    bool SetActive(string name);
    GameMode? GetActiveMode();
    string GetActiveString();
    GameMode? Get(string name);
    List<string>? GetAll();
}