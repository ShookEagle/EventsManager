using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Timers;
using EventsManager.api.plugin;
using EventsManager.api.plugin.services;
using EventsManager.plugin.menus.components;
using EventsManager.plugin.menus.style;
using RMenu;
using RMenu.Enums;

namespace EventsManager.plugin.menus;

public class MapsMenu : PaginatedMenuBase
{
    private readonly IEventsManager _plugin;
    private readonly IMapGroupService _mapGroupService;
    private readonly IServerStateService _serverStateService;
    private readonly IAnnouncerService _announcerService;
    private readonly ILoggerService _loggerService;
    private readonly CCSPlayerController _player;

    public MapsMenu(IEventsManager plugin, CCSPlayerController player, MenuValue[] header, RMenuBase? parent) : base(plugin, player, header, parent)
    {
        _mapGroupService = plugin.GetMapGroupService();
        _serverStateService = plugin.GetServerStateService();
        _announcerService = plugin.GetAnnouncerService();
        _loggerService = plugin.GetLoggerService();
        _plugin = plugin;
        _player = player;
        SetItems(_mapGroupService.GetActiveGroup()!.Select(m => m.Name).ToList());
    }

    protected override MenuItem CreateMenuItem(string item)
    {
        var isActive = item == Server.MapName;
        var color = isActive ? Theme.AccentGreen : Theme.TextPrimary;
        return new MenuItem(MenuItemType.Button, new MenuValue(item, color));
    }

    protected override void OnItemSelected(string selected)
    {
        _announcerService.Announce(_player.PlayerName, selected, "has changed the map to", ". Changing in 3 seconds", "yellow");
        _loggerService.Map(_player, selected);
        _serverStateService.UpdateMapAsync(selected);
        _plugin.GetBase().AddTimer(3f, () =>
        { _mapGroupService.SwitchMap(selected);
        }, TimerFlags.STOP_ON_MAPCHANGE);
        base.OnItemSelected(selected);
    }
}