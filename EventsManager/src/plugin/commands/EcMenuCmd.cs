using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using EventsManager.api.plugin;
using EventsManager.plugin.extensions;
using EventsManager.plugin.menus;

namespace EventsManager.plugin.commands;

public class EcMenuCmd(IEventsManager plugin) : Command(plugin)
{
    public override void OnCommand(CCSPlayerController? executor, CommandInfo info)
    {
        if (executor == null || !executor.IsReal()) return;
        new EcMenu(plugin, executor).Show();
    }
}