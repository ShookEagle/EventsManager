using System.Drawing;
using RMenu;
using RMenu.Enums;

namespace EventsManager.plugin.menus.style;

public static class Theme
{
    public static readonly Color TextPrimary        = Color.FromArgb(255, 255, 255);    // white
    public static readonly Color PlaceholderText    = Color.FromArgb(136, 136, 136);    // #888
    public static readonly Color TextDark           = Color.FromArgb(68, 68, 68);       // #444
    
    public static readonly Color PrimaryBlue        = Color.FromArgb(58, 110, 165);     // #3a6ea5
    public static readonly Color AccentBlue         = Color.FromArgb(93, 157, 253);     // #5d9dfd
    public static readonly Color AccentYellow       = Color.FromArgb(244, 197, 66);     // #f4c542
    public static readonly Color AccentGreen        = Color.FromArgb(59, 165, 93);      // #3ba55d
    public static readonly Color AccentRed          = Color.FromArgb(255, 102, 102);    // #ff6666
    public static readonly Color AccentDarkRed      = Color.FromArgb(255, 51, 51);      // #ff3333
    
    public static readonly MenuItem BlankItem       = new(MenuItemType.Text, new MenuValue("Empty", TextDark));
}

public static class ThemeExtensions
{
    private static string ToHex(this Color color) => $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    public static string ToHtmlColor(this Color color) => $"<font color=\"{color.ToHex()}\">";
}