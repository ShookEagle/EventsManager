using CounterStrikeSharp.API;
using EventsManager.api.plugin;
using EventsManager.api.plugin.services;
using EventsManager.plugin.models;
using Microsoft.Extensions.Logging;

namespace EventsManager.plugin.services;

public class CommandPackService(IWebService api, EventsManager plugin) : ICommandPackService
{
    private Dictionary<string, CommandPack> CommandPacks { get; set; } = new();
    private HashSet<string> ActivePacks { get; } = [];
    
    public async Task<bool> LoadAsync()
    {
        var packs = await api.GetAsync<Dictionary<string, CommandPack>>("commandpacks.php");
        if (packs == null)
        {
            plugin.Logger.LogError("[CommandPacks] Failed to Load.");
            return false;
        }

        CommandPacks = packs;
        plugin.Logger.LogInformation($"[CommandPacks] Successfully loaded {packs.Count} Command Packs.");
        return true;
    }
    
    public bool Enable(string name)
    {
        if (!CommandPacks.TryGetValue(name, out var pack)) return false;
        foreach (var cmd in pack.OnExecCmds)
            Server.ExecuteCommand(cmd);
        return ActivePacks.Add(name);
    }
    
    public void EnableMany(IEnumerable<string> packNames)
    {
        foreach (var name in packNames)
            Enable(name);
    }
    
    public void EnableOnly(IEnumerable<string> newActive)
    {
        var newSet = new HashSet<string>(newActive);

        foreach (var name in ActivePacks.Except(newSet))
            Disable(name);

        foreach (var name in newSet.Except(ActivePacks))
            Enable(name);
    }

    public bool Disable(string name)
    {
        if (!CommandPacks.TryGetValue(name, out var pack)) return false;
        foreach (var cmd in pack.OffExecCmds)
            Server.ExecuteCommand(cmd);
        return ActivePacks.Remove(name);
    }

    public void Toggle(string name)
    {
        if (ActivePacks.Contains(name)) Disable(name);
        else Enable(name);
    }

    public void DisableAll()
    {
        foreach (var name in ActivePacks.ToList())
            Disable(name);
    }

    public bool IsActive(string name) => ActivePacks.Contains(name);
    public List<string> GetAll() => CommandPacks.Keys.ToList();
}