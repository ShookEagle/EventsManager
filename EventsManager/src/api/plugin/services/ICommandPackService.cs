namespace EventsManager.api.plugin.services;

public interface ICommandPackService
{
    Task<bool> LoadAsync();
    bool Enable(string name);
    void EnableMany(IEnumerable<string> packNames);
    void EnableOnly(IEnumerable<string> newActive);
    bool Disable(string name);
    void Toggle(string name);
    void DisableAll();
    bool IsActive(string name);
    List<string> GetAll();
}