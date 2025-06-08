namespace EventsManager.api.plugin.services;

public interface IServerStateService
{
    Task<bool> PushInitialStateAsync();
    Task<bool> LoadAsync();
    Task<bool> UpdateModeAsync(string mode);
    Task<bool> UpdateMapAsync(string map);
    Task<bool> PushAsync();
}