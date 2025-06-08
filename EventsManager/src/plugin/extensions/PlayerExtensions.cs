using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using EventsManager.maul.enums;
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
    
    public static MaulPermission GetRank(this CCSPlayerController player) {
        if (!player.IsReal()) return MaulPermission.None;
        if (AdminManager.PlayerInGroup(player, "#ego/root")
            || AdminManager.PlayerHasPermissions(player, "@css/root"))
            return MaulPermission.Root;
        if (AdminManager.PlayerInGroup(player, "#ego/executive"))
            return MaulPermission.Executive;
        if (AdminManager.PlayerInGroup(player, "#ego/directory"))
            return MaulPermission.Director;
        if (AdminManager.PlayerInGroup(player, "#ego/commgr"))
            return MaulPermission.CommunityManager;
        if (AdminManager.PlayerInGroup(player, "#ego/srmanager"))
            return MaulPermission.SeniorManager;
        if (AdminManager.PlayerInGroup(player, "#ego/manager"))
            return MaulPermission.Manager;
        if (AdminManager.PlayerInGroup(player, "#ego/advisor"))
            return MaulPermission.Advisor;
        if (AdminManager.PlayerInGroup(player, "#ego/ego"))
            return MaulPermission.EGO;
        if (AdminManager.PlayerInGroup(player, "#ego/eg")) return MaulPermission.EG;
        if (AdminManager.PlayerInGroup(player, "#ego/e")) return MaulPermission.E;
        return MaulPermission.None;
    }


    public static bool IsEC(this CCSPlayerController player)
    { return player.IsReal() && player.GetRank() >= MaulPermission.Manager; }

}