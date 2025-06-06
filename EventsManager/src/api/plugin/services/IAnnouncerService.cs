namespace EventsManager.api.plugin.services;

public interface IAnnouncerService
{
    void Announce(string admin, string target, string action, string suffix = "", string actionColor = "bluegrey");
}