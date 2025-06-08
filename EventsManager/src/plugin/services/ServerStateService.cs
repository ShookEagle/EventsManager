using CounterStrikeSharp.API;
using EventsManager.api.plugin;
using EventsManager.api.plugin.services;
using EventsManager.plugin.models;
using Microsoft.Extensions.Logging;

namespace EventsManager.plugin.services;

public class ServerStateService(IWebService api, EventsManager plugin) : IServerStateService
{
    private ServerState State { get; set; } = new();

    public async Task<bool> PushInitialStateAsync()
    {
        State.Mode = plugin.GetGameModesService().GetActiveString();
        State.Map = Server.MapName;
        State.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        
        var success = await api.PostAsync("state.php", State);
        if (!success) return false;
        plugin.Logger.LogInformation("[STATE] Successfully Sent Server State");
        return true;
    }
    
    public async Task<bool> LoadAsync()
    {
        var state = await api.GetAsync<ServerState>("state.php");
        if (state is null) return false;
        State = state;
        return true;
    }

    public async Task<bool> UpdateModeAsync(string mode)
    {
        State.Mode = mode;
        State.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return await api.PostAsync("state.php", State);
    }
    
    public async Task<bool> UpdateMapAsync(string map)
    {
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