using CounterStrikeSharp.API;
using EventsManager.api.plugin;
using EventsManager.api.plugin.services;
using EventsManager.plugin.models;
using Microsoft.Extensions.Logging;

namespace EventsManager.plugin.services;

public class CommandPackService(IWebService api, EventsManager plugin) : ICommandPackService
{
    public Dictionary<string, CommandPack> CommandPacks { get; private set; } = new();
    public HashSet<string> ActivePacks { get; } = [];
    
    public async Task<bool> LoadAsync()
    {
        var packs = await api.GetAsync<Dictionary<string, CommandPack>>("commandpacks.php");
        if (packs == null)
        {
            plugin.Logger.LogError("[CommandPacks] Failed to Load.");
            return false;
        }

        CommandPacks = packs;
        return true;
    }
    
    public bool Enable(string name)
    {
        if (!CommandPacks.TryGetValue(name, out var pack)) return false;
        foreach (var cmd in pack.OnExecCmds)
            Server.ExecuteCommand(cmd);
        return ActivePacks.Add(name);
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
}