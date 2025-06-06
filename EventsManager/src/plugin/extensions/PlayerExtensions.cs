using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using EventsManager.plugin.utils;
using Microsoft.Extensions.Localization;

namespace EventsManager.plugin.extensions;

public static class PlayerExtensions
{
    public static void PrintLocalizedChat(this CCSPlayerController? controller,
        IStringLocalizer localizer, string local, params object[] args) {
        if (controller == null || !controller.IsReal(false)) return;
        string message = localizer[local, args];
        message = message.Replace("%prefix%", localizer["prefix"]);
        message = StringUtils.ReplaceChatColors(message);
        controller.PrintToChat(message);
    }

    public static bool IsReal(this CCSPlayerController? player, bool bot = true) {
        //  Do nothing else before this:
        //  Verifies the handle points to an entity within the global entity list.
        if (player == null || !player.IsValid) return false;

        if (player.Connected != PlayerConnectedState.PlayerConnected) return false;

        if ((player.IsBot || player.IsHLTV) && !bot) return false;

        return true;
    }

    public static bool IsEC(this CCSPlayerController player)
    { return player.IsReal() && AdminManager.PlayerHasPermissions(player, "#ego/manager"); }

}