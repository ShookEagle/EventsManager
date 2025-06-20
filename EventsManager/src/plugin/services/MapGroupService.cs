using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using EventsManager.api.plugin.services;
using EventsManager.plugin.models;
using Microsoft.Extensions.Logging;

namespace EventsManager.plugin.services;

public class MapGroupService(IWebService api, BasePlugin plugin) : IMapGroupService
{
    private Dictionary<string, MapGroup> MapGroups { get; set; } = new();
    private string ActiveGroup { get; set; } = "Active";

    public async Task<bool> LoadAsync()
    {
        var groups = await api.GetAsync<Dictionary<string, MapGroup>>("mapgroups.php");
        if (groups == null) { plugin.Logger.LogError("[MapGroups] Failed to Load."); return false; }

        MapGroups = groups;
        if (!MapGroups.ContainsKey(ActiveGroup))
            ActiveGroup = MapGroups.Keys.FirstOrDefault() ?? "Active"; //always fallback to `Active`

        plugin.Logger.LogInformation($"[MapGroups] Successfully loaded {groups.Count} Map Groups.");
        return true;
    }

    public void SetActiveGroup(string groupName)
    {
        if (MapGroups.ContainsKey(groupName))
            ActiveGroup = groupName;
    }

    public MapGroup? GetActiveGroup() => MapGroups.TryGetValue(ActiveGroup, out var group) ? group : null;

    public void SwitchMap(string mapName)
    {
        var mapId = GetActiveGroup()?.FirstOrDefault(m => m.Name.Equals(mapName))?.WorkshopId;
        if (mapId != "") 
            Server.ExecuteCommand($"host_workshop_map {mapId}");
        Server.ExecuteCommand($"changelevel {mapName}");
    }
}