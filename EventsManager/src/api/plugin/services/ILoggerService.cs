using CounterStrikeSharp.API.Core;

namespace EventsManager.api.plugin.services;

public interface ILoggerService
{
    void Info(string message, Dictionary<string, object>? meta = null);
    void Warn(string message, Dictionary<string, object>? meta = null);
    void Error(string message, Dictionary<string, object>? meta = null);
    void Action(string message, Dictionary<string, object>? meta = null);
    void Mode(CCSPlayerController executor, string mode, Dictionary<string, object>? meta = null);
    void Map(CCSPlayerController executor, string map, Dictionary<string, object>? meta = null);
}