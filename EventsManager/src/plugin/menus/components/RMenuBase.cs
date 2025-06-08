using CounterStrikeSharp.API.Core;
using EventsManager.api.plugin;
using EventsManager.plugin.menus.components;
using RMenu;
using RMenu.Enums;

namespace EventsManager.plugin.menus;

public abstract class RMenuBase
{
    private CCSPlayerController Player { get; }
    protected MenuBase Menu { get; }
    public MenuBase RawMenu => Menu;
    private MenuBase? Parent { get; }

    protected RMenuBase(IEventsManager plugin, CCSPlayerController player, MenuValue[] header, MenuBase? parent = null)
    {
        Player = player;
        Parent = parent;
        
        var options = new MenuOptions
        {
            DisplayItemsInHeader = true,
            BlockMovement = true,
            BlockJump = true,
            HeaderFontSize = MenuFontSize.M,
            ItemFontSize = MenuFontSize.M,
            FooterFontSize = MenuFontSize.S,
            Cursor = Elements.Cursor,
            Selector = Elements.Selector
        };

        Menu = new MenuBase(header: header,footer: Elements.Footer, options: options);
    }
    
    protected abstract void Build();
    public void Show()
    {
        Build();
        RMenu.Menu.Add(Player, Menu, OnAction);
    }

    protected abstract void OnAction(CCSPlayerController player, MenuBase menu, MenuAction action);

    protected void GoBack()
    {
        if (Parent != null)
            RMenu.Menu.Add(Player, Parent);
    }
}