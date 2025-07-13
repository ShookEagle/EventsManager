using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using CounterStrikeSharp.API;
using EventsManager.api.plugin;
using EventsManager.api.plugin.services;
using EventsManager.plugin.models;
using Microsoft.Extensions.Logging;

namespace EventsManager.plugin.services;

public class WebService : IWebService {
    private readonly IEventsManager _plugin;
    private readonly HttpClient _http;
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    
    private ClientWebSocket _socket;
    private readonly Uri _uri = new("ws://localhost:8080");
    
    public WebService(HttpClient httpClient, IEventsManager plugin)
    {
        this._plugin = plugin;
        _http = httpClient;
        _http.BaseAddress = new Uri($"{plugin.Config.WebServerUrl}:{plugin.Config.WebServerPort}/api/");
        _socket = new ClientWebSocket();
    }
    
    public async Task<T?> GetAsync<T>(string endpoint)
    {
        try
        {
            var response = await _http.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode) return default;
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, _options);
        }
        catch { return default; }
    }
    public async Task<bool> PostAsync<T>(string endpoint, T data)
    {
        try
        {
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var response = await _http.PostAsync(endpoint, content);
            return response.IsSuccessStatusCode;
        }
        catch { return false; }
    }
    
    public async void Connect()
    {
        try
        {
            await _socket.ConnectAsync(_uri, CancellationToken.None);
            _plugin.GetBase().Logger.LogInformation("WebSocket connected");

            _ = Task.Run(ReceiveLoop); // don’t block the main thread
        }
        catch (Exception e)
        { _plugin.GetBase().Logger.LogError("WebSocket error: " + e); }
    }

    private async Task ReceiveLoop()
    {
        var buffer = new byte[1024];

        while (_socket.State == WebSocketState.Open)
        {
            try
            {
                var result = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                var msg = Encoding.UTF8.GetString(buffer, 0, result.Count);

                _plugin.GetBase().Logger.LogInformation("WebSocket message received");

                var command = JsonSerializer.Deserialize<CommandMessage>(msg, _options);
                if (command is null || string.IsNullOrWhiteSpace(command.Action))
                {
                    _plugin.GetBase().Logger.LogWarning("Invalid or malformed WebSocket command");
                    continue;
                }

                HandleCommand(command);
            }
            catch (Exception ex)
            {
                _plugin.GetBase().Logger.LogError("WebSocket message handling error: " + ex);
            }
        }
    }

    private void HandleCommand(CommandMessage cmd)
    {
        switch (cmd.Action)
        {
            case "test":
                if (cmd.Params?.TryGetValue("message", out var message) == true)
                {
                    _plugin.GetDispatcher().Enqueue(() =>
                    {
                        try { Server.PrintToChatAll($"[Dashboard] {message}"); }
                        catch (Exception ex)
                        { _plugin.GetBase().Logger.LogError("Failed to broadcast message: " + ex); }
                    });
                }
                break;

            default:
                _plugin.GetBase().Logger.LogWarning($"Unknown WebSocket action received: {cmd.Action}");
                break;
        }
    }
}