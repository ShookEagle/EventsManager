using CounterStrikeSharp.API.Core;
using EventsManager.api.plugin;
using RMenu;
using RMenu.Enums;
using Timer = CounterStrikeSharp.API.Modules.Timers.Timer;

namespace EventsManager.plugin.menus.components;

public abstract class RMenuBase
{
    private CCSPlayerController Player { get; }
    public MenuBase Menu { get; }
    private RMenuBase? Parent { get; }
    private IEventsManager Plugin { get; }

    protected RMenuBase(IEventsManager plugin, CCSPlayerController player, MenuValue[] header, RMenuBase? parent = null)
    {
        Plugin = plugin;
        Player = player;
        Parent = parent;
        
        var options = new MenuOptions
        {
            DisplayItemsInHeader = true,
            BlockJump = true,
            BlockMovement = true,
            HeaderFontSize = MenuFontSize.M,
            ItemFontSize = MenuFontSize.SM,
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
        if (Parent == null) return;
        Plugin.GetBase().AddTimer(0.1f, () =>
        {
            RMenu.Menu.Add(Player, Parent.Menu, Parent.OnAction);
        });
    }
}