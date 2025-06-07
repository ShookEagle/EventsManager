using System.Drawing;
using CounterStrikeSharp.API.Core;
using EventsManager.api.plugin;
using RMenu;
using RMenu.Enums;

namespace EventsManager.plugin.menus;

public class EcMenu(IEventsManager plugin, CCSPlayerController player) : RMenuBase(player, new MenuValue("EC Menu", Color.Blue))
{
    protected override void Build()
    {
        Menu.AddItem(new MenuItem(MenuItemType.Button, new MenuValue("Modes")));
        Menu.AddItem(new MenuItem(MenuItemType.Button, new MenuValue("Maps")));
        Menu.AddItem(new MenuItem(MenuItemType.Button, new MenuValue("Settings")));
        Menu.AddItem(new MenuItem(MenuItemType.Button, new MenuValue("Tools")));
    }
}