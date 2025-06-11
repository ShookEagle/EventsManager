using CounterStrikeSharp.API.Core;
using EventsManager.api.plugin;
using EventsManager.api.plugin.services;
using EventsManager.plugin.menus.components;
using EventsManager.plugin.menus.style;
using RMenu;
using RMenu.Enums;

namespace EventsManager.plugin.menus;

public class SettingsMenu : PaginatedMenuBase
{
    private readonly IEventsManager _plugin;
    private readonly ICommandPackService _commandPackService;
    private readonly IServerStateService _serverStateService;
    private readonly IAnnouncerService _announcerService;
    private readonly ILoggerService _loggerService;
    private readonly CCSPlayerController _player;
    
    public SettingsMenu(IEventsManager plugin, CCSPlayerController player, MenuValue[] header, RMenuBase? parent) 
        : base(plugin, player, header, parent)
    {
        _commandPackService = plugin.GetCommandPackService();
        _serverStateService = plugin.GetServerStateService();
        _announcerService = plugin.GetAnnouncerService();
        _loggerService = plugin.GetLoggerService();
        _plugin = plugin;
        _player = player;
        SetItems(_commandPackService.GetAll().OrderBy(x => x));
    }

    protected override MenuItem CreateMenuItem(string item)
    {
        var isActive = _commandPackService.IsActive(item);
        var iconString = $"{(isActive ? $"{Theme.AccentGreen.ToHtmlColor()}✔" : $"{Theme.AccentRed.ToHtmlColor()}✘")}";
        return new MenuItem(MenuItemType.Button, new MenuValue($"{item} - {iconString}", Theme.TextPrimary));
    }

    protected override void OnItemSelected(string selected)
    {
        _commandPackService.Toggle(selected);
        var isActive = _commandPackService.IsActive(selected);
        _announcerService.Announce(_player.PlayerName, selected, isActive ? "enabled" : "disabled", ".", 
            isActive ? "lime" : "lightred");
        _loggerService.Settings(_player, selected, isActive);
        base.OnItemSelected(selected);
    }
}