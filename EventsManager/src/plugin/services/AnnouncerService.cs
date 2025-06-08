using CounterStrikeSharp.API;
using EventsManager.api.plugin;
using EventsManager.api.plugin.services;
using EventsManager.maul.enums;
using EventsManager.plugin.extensions;

namespace EventsManager.plugin.services;

public class AnnouncerService(IEventsManager plugin) : IAnnouncerService
{
    public void Announce(string admin, string target, string action,
        string suffix = "", string actionColor = "bluegrey") {
        foreach (var player in
                 Utilities.GetPlayers().Where(player => admin != player.PlayerName)) {
            player.PrintLocalizedChat(plugin.GetBase().Localizer,
                "admin_action_format",
                player.GetRank() >= MaulPermission.EG ? admin : "EC", actionColor, action, target,
                suffix);
        }
    }

}