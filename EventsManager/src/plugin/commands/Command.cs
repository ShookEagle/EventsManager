using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using EventsManager.api.plugin;

namespace EventsManager.plugin.commands;

public abstract class Command(IEventsManager plugin)
{
    protected readonly IEventsManager Plugin = plugin;
    public string? Description => null;
    public abstract void OnCommand(CCSPlayerController? executor, CommandInfo info);
}