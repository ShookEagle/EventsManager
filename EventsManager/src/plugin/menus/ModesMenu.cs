using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using EventsManager.api.plugin;
using EventsManager.api.plugin.services;
using EventsManager.plugin.menus.components;
using EventsManager.plugin.menus.style;
using RMenu;
using RMenu.Enums;

namespace EventsManager.plugin.menus;

public class ModesMenu : PaginatedMenuBase
{
    private IGameModeService _gameModeService;
    
    public ModesMenu(IEventsManager plugin, CCSPlayerController player, MenuValue[] header, MenuBase? parent = null) 
        : base(plugin, player, header, parent)
    {
        _gameModeService = plugin.GetGameModesService();
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
        Server.PrintToChatAll($"Selected {selected}");
    }
}