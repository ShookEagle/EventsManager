namespace EventsManager.api.plugin.services;

public interface IServerStateService
{
    Task<bool> LoadAsync();
    Task<bool> UpdateAsync(string mode, string map);
    Task<bool> PushAsync();
    Task<bool> PushInitialStateAsync();
}