namespace EventsManager.api.plugin.services;

public interface ICommandPackService
{
    Task<bool> LoadAsync();
    bool Enable(string name);
    bool Disable(string name);
    void Toggle(string name);
    void DisableAll();
    bool IsActive(string name);
}