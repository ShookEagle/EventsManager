using EventsManager.plugin.models;

namespace EventsManager.api.plugin.services;

public interface IMapGroupService
{
    Task<bool> LoadAsync();
    void SetActiveGroup(string groupName);
    MapGroup? GetActiveGroup();
}