using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Entities;
using EventsManager.api.plugin;
using EventsManager.api.plugin.services;
using EventsManager.plugin.extensions;
using EventsManager.plugin.menus.components;
using EventsManager.plugin.menus.style;
using RMenu;
using RMenu.Enums;

namespace EventsManager.plugin.menus;

public class ModesMenu : PaginatedMenuBase
{
    private readonly IEventsManager _plugin;
    private readonly IGameModeService _gameModeService;
    private readonly IServerStateService _serverStateService;
    private readonly IAnnouncerService _announcerService;
    private readonly ILoggerService _loggerService;
    private readonly CCSPlayerController _player;
    
    public ModesMenu(IEventsManager plugin, CCSPlayerController player, MenuValue[] header, MenuBase? parent = null) 
        : base(plugin, player, header, parent)
    {
        _gameModeService = plugin.GetGameModesService();
        _serverStateService = plugin.GetServerStateService();
        _announcerService = plugin.GetAnnouncerService();
        _loggerService = plugin.GetLoggerService();
        _plugin = plugin;
        _player = player;
        SetItems((_gameModeService.GetAll() ?? throw new InvalidOperationException()).OrderBy(x => x));
    }
    protected override MenuItem CreateMenuItem(string item)
    {
        var isActive = item == _gameModeService.GetActiveString();
        var color = isActive ? Theme.AccentGreen : Theme.TextPrimary;
        return new MenuItem(MenuItemType.Button, new MenuValue(item, color));
    }

    protected override void OnItemSelected(string selected)
    {
        if (_gameModeService.SetActive(selected))
        {
            _announcerService.Announce(_player.PlayerName, selected, "has changed the mode to", ".", "lightpurple");
            _loggerService.Mode(_player, selected);
            _serverStateService.UpdateModeAsync(selected);
        }
        _player.PrintLocalizedChat(_plugin.GetBase().Localizer, "error_try_again", "Mode Activation Failed");
    }
}