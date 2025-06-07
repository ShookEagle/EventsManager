using CounterStrikeSharp.API.Core;
using EventsManager.plugin.menus.components;
using RMenu;
using RMenu.Enums;

namespace EventsManager.plugin.menus;

public abstract class RMenuBase
{
    protected CCSPlayerController Player { get; }
    protected MenuBase Menu { get; }
    public MenuBase RawMenu => Menu;

    protected RMenuBase(CCSPlayerController player, MenuValue[] header)
    {
        Player = player;
        
        var options = new MenuOptions
        {
            DisplayItemsInHeader = true,
            BlockMovement = true,
            HeaderFontSize = MenuFontSize.M,
            ItemFontSize = MenuFontSize.M,
            FooterFontSize = MenuFontSize.S,
            Cursor = Elements.Cursor,
            Selector = Elements.Selector
        };

        Menu = new MenuBase(header: header,footer: Elements.Footer, options: options);
        
        Build();
    }
    
    protected abstract void Build();
    public void Show() { RMenu.Menu.Add(Player, Menu, OnAction); }
    protected virtual void OnAction(CCSPlayerController player, MenuBase menu, MenuAction action) { }
}