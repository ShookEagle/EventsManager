using System.Drawing;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using EventsManager.api.plugin;
using RMenu;
using RMenu.Enums;

namespace EventsManager.plugin.menus;

public class EcMenu(IEventsManager plugin, CCSPlayerController player) : RMenuBase(plugin ,player, [new MenuValue("EC Menu", Color.Blue)])
{
    protected override void Build()
    {
        Menu.AddItem(new MenuItem(MenuItemType.Button, new MenuValue("Modes")));
        Menu.AddItem(new MenuItem(MenuItemType.Button, new MenuValue("Maps")));
        Menu.AddItem(new MenuItem(MenuItemType.Button, new MenuValue("Settings")));
        Menu.AddItem(new MenuItem(MenuItemType.Button, new MenuValue("Tools")));
    }

    protected override void OnAction(CCSPlayerController player, MenuBase menu, MenuAction action)
    {
        if (action != MenuAction.Select) return;
        
        switch (menu.SelectedItem?.Index)
        {
            case 0:
                new ModesMenu(plugin, player, [new MenuValue("EC Menu", Color.Blue)], Menu).Show();
                break;
        }
    }
}