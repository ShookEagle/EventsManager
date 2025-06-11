using CounterStrikeSharp.API.Core;
using EventsManager.api.plugin;
using EventsManager.api.plugin.services;
using EventsManager.plugin.extensions;

namespace EventsManager.plugin.listeners;

public class PlayerStateListener
{
    private readonly IEventsManager _plugin;
    private readonly IPlayerStateService _playerStateService;

    public PlayerStateListener(IEventsManager plugin)
    {
        _plugin = plugin;
        _playerStateService = plugin.GetPlayerStateService();
        
        plugin.GetBase().RegisterEventHandler<EventPlayerConnectFull>(OnPlayerConnect);
        plugin.GetBase().RegisterEventHandler<EventPlayerDisconnect>(OnPlayerDisconnect);
        plugin.GetBase().RegisterEventHandler<EventPlayerTeam>(OnTeam);
    }

    private HookResult OnPlayerConnect(EventPlayerConnectFull @event, GameEventInfo info)
    {
        var player = @event.Userid;
        if (player == null || !player.IsReal()) return HookResult.Continue;

        _playerStateService.GetOrCreate(player);

        _playerStateService.SchedulePush();
        return HookResult.Continue;
    }

    private HookResult OnPlayerDisconnect(EventPlayerDisconnect @event, GameEventInfo info)
    {
        var player = @event.Userid;
        if (player == null || !player.IsReal()) return HookResult.Continue;

        _playerStateService.Remove(player.Slot);

        _playerStateService.SchedulePush();
        return HookResult.Continue;
    }

    private HookResult OnTeam(EventPlayerTeam @event, GameEventInfo info)
    {
        var player = @event.Userid;
        if (player == null || !player.IsReal()) return HookResult.Continue;

        var state = _playerStateService.GetOrCreate(player);
        state.Team = player.Team.ToString();
        
        _playerStateService.SchedulePush();
        return HookResult.Continue;
    }
}