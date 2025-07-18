using CounterStrikeSharp.API;
using EventsManager.api.plugin;
using EventsManager.api.plugin.services;
using EventsManager.maul.enums;
using EventsManager.plugin.extensions;
using Microsoft.Extensions.Logging;

namespace EventsManager.plugin.services;

public class AnnouncerService(IEventsManager plugin) : IAnnouncerService
{
    public void Announce(string admin, string target, string action, 
        string suffix = "", string actionColor = "bluegrey")
    {
        foreach (var player in Utilities.GetPlayers().Where(player => player.IsReal()))
        {
            plugin.GetDispatcher().Enqueue(() =>
            {
                player.PrintLocalizedChat(plugin.GetBase().Localizer, "admin_action_format",
                    player.GetRank() >= MaulPermission.EG ? admin : "EC",
                    action,
                    "{" + actionColor + "}",
                    target,
                    suffix);
            }); // why must cs# run everything off main-thread :c I feel like I'm working in Unity
        }
    }
}