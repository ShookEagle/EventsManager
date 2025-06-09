using System.Drawing;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using EventsManager.api.plugin;
using EventsManager.plugin.menus.components;
using RMenu;
using RMenu.Enums;

namespace EventsManager.plugin.menus;

public class EcMenu(IEventsManager plugin, CCSPlayerController player) : RMenuBase(plugin ,player, [new MenuValue("EC Menu", Color.Blue)])
{
    private readonly IEventsManager _plugin = plugin;

    protected override void Build()
    {
        Menu.Items.Clear();
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
                new ModesMenu(_plugin, player, [new MenuValue("EC Menu", Color.Blue)], this).Show();
                break;
        }
    }
}