using EventsManager.plugin.menus.style;
using RMenu;

namespace EventsManager.plugin.menus.components;

public static class Elements
{
    public static readonly MenuValue[] Cursor =
    {
        new("►", Theme.PrimaryBlue),
        new("◄", Theme.PrimaryBlue)
    };

    public static readonly MenuValue[] Selector =
    {
        new("[ ", Theme.PrimaryBlue),
        new(" ]", Theme.PrimaryBlue)
    };

    public static readonly MenuValue[] Footer =
    {
        new("Scroll - ",    Theme.PlaceholderText),
        new("W/S ",         Theme.AccentBlue),
        new("| Select - ",  Theme.PlaceholderText),
        new("Jump ",        Theme.AccentGreen),
        new("| Back - ",    Theme.PlaceholderText),
        new("Crouch ",      Theme.AccentYellow),
        new("| Exit - ",    Theme.PlaceholderText),
        new("Tab ",         Theme.AccentDarkRed)
    };
}