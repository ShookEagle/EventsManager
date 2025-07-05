using CounterStrikeSharp.API;
using EventsManager.api.plugin.services;
using EventsManager.plugin.models;
using Microsoft.Extensions.Logging;

namespace EventsManager.plugin.services;

public class GameModeService(IWebService api, EventsManager plugin) : IGameModeService
{
    private readonly IMapGroupService _mapGroupService = plugin.GetMapGroupService();
    private readonly ICommandPackService _commandPackService = plugin.GetCommandPackService();
    private Dictionary<string, GameMode> GameModes { get; set; } = new();
    public string ActiveModeName { get; set; } = "Default";

    public async Task<bool> LoadAsync()
    {
        var modes = await api.GetAsync<Dictionary<string, GameMode>>("modes.php");
        if (modes == null)
        {
            plugin.Logger.LogError("[GameModes] Failed to Load.");
            return false;
        }
        
        GameModes = modes;
        if (!GameModes.ContainsKey(ActiveModeName))
            ActiveModeName = GameModes.Keys.FirstOrDefault() ?? "Default";
        
        plugin.Logger.LogInformation($"[GameModes] Successfully loaded {modes.Count} Game Modes.");
        return true;
    }
    
    public bool SetActive(string name)
    {
        if (!GameModes.ContainsKey(name)) return false;
        ActiveModeName = name;
        ApplyGameMode(Get(ActiveModeName));
        
        return true;
    }

    public GameMode? GetActiveMode() =>
        GameModes.TryGetValue(ActiveModeName, out var mode) ? mode : null;

    public string GetActiveString() => ActiveModeName;

    public GameMode? Get(string name) =>
        GameModes.TryGetValue(name, out var mode) ? mode : null;

    public List<string>? GetAll() => GameModes.Keys.ToList();

    private void ApplyGameMode(GameMode? mode)
    {
        if (mode is null) return;
        
        // 0. Unload Plugins and Default Server
        Server.ExecuteCommand("exec \"utils/unload_plugins.cfg\"");
        Server.ExecuteCommand("exec \"utils/server_default.cfg\"");
        
        // 1. Change Map Group
        _mapGroupService.SetActiveGroup(mode.MapGroup);

        // 2. Apply Command Packs
        if (mode.CommandPacks != null)
            _commandPackService.EnableOnly(mode.CommandPacks!);
        
        // 3. Apply Mode Specific Commands
        var commands = FlattenSettings(mode.Settings);
        foreach (var cmd in commands)
            Server.ExecuteCommand(cmd);
        
        // 4. Load Plugins 
        if (mode.Plugins == null) return;
        foreach (var path in mode.Plugins)
            Server.ExecuteCommand($"css_plugins load \"plugins/disabled/{path}\"");
    }
    
    private static List<string> FlattenSettings(Dictionary<string, object?>? settings)
    {
        var result = new List<string>();
        if (settings is null) return result;
        
        foreach (var (key, value) in settings)
        {
            if (value == null) continue;
                
            var arg = value switch
            {
                string s => $"\"{s}\"",
                int or float or double or bool => value.ToString()!,
                _ => $"\"{value}\""
            };
            result.Add($"{key} {arg}");
        }
        return result;
    }
}