using CounterStrikeSharp.API;
using EventsManager.api.plugin;
using EventsManager.api.plugin.services;
using EventsManager.plugin.models;

namespace EventsManager.plugin.services;

public class ServerStateService(IWebService api, IEventsManager plugin) : IServerStateService
{
    private ServerState State { get; set; } = new();

    public async Task<bool> PushInitialStateAsync()
    {
        State.Mode = plugin.GetGameModesService().GetActiveString();
        State.Map = Server.MapName;
        State.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return await api.PostAsync("state.php", State);
    }
    
    public async Task<bool> LoadAsync()
    {
        var state = await api.GetAsync<ServerState>("state.php");
        if (state is null) return false;
        State = state;
        return true;
    }

    public async Task<bool> UpdateAsync(string mode, string map)
    {
        State.Mode = mode;
        State.Map = map;
        State.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return await api.PostAsync("state.php", State);
    }

    public async Task<bool> PushAsync()
    {
        State.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return await api.PostAsync("state.php", State);
    }
}