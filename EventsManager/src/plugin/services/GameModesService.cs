using EventsManager.api.plugin;
using EventsManager.api.plugin.services;
using EventsManager.plugin.models;
using Microsoft.Extensions.Logging;

namespace EventsManager.plugin.services;

public class GameModesService : IGameModesService
{
    private IEventsManager _plugin;
    private Dictionary<string, GameMode> _modes = new();
    private GameMode? _activeMode;

    public GameModesService(IEventsManager plugin)
    {
        _plugin = plugin;
    }
    
    public bool TryGetMode(string key, out GameMode? mode)
    {
        return _modes.TryGetValue(key, out mode);
    }
    
    public void ApplyMode(string key)
    {
        //Revert to Saved Defaults
        //Unload Unecessary Plugins
        //Apply New Mode Settings
        //Start New Mode Plugins
        //Change MapGroup
    }
    
    public GameMode? GetActiveMode() => _activeMode;
    public IEnumerable<string> GetAvailableModeKeys() => _modes.Keys;
}