using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using EventsManager.api.plugin;
using EventsManager.api.plugin.services;
using EventsManager.plugin.extensions;
using EventsManager.plugin.models;

namespace EventsManager.plugin.commands;

public class LogsCmd(IEventsManager plugin) : Command(plugin)
{
    private readonly IWebService _api = plugin.GetWebService();
    public override async void OnCommand(CCSPlayerController? executor, CommandInfo info)
    {
        if (!executor.IsReal() || executor == null) return;

        var response = await _api.GetAsync<List<LogEntry>>("logs.php");
        if (response == null)
        {
            info.ReplyLocalized(Plugin.GetBase().Localizer, "error_try_again", "Failed to fetch logs");
            return;
        }
        
        executor.PrintToConsole($"--- Last {response.Count} Logs ---");
        foreach (var entry in response)
            executor.PrintToConsole($"[{entry.Type}] {entry.Message}");
    }
}