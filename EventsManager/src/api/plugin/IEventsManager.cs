using CounterStrikeSharp.API.Core;
using EventsManager.api.plugin.services;
using EventsManager.plugin;
using MAULActainShared.plugin;

namespace EventsManager.api.plugin;

public interface IEventsManager : IPluginConfig<EventsManagerConfig>
{
    BasePlugin GetBase();
    IActain GetActain();
    IMainThreadDispatcher GetDispatcher();
    IWebService GetWebService();
    IMapGroupService GetMapGroupService();
    ICommandPackService GetCommandPackService();
    IGameModeService GetGameModesService();
    IServerStateService GetServerStateService();
    IPlayerStateService GetPlayerStateService();
    IAnnouncerService GetAnnouncerService();
    ILoggerService GetLoggerService();
}