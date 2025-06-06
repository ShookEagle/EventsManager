using System.Text.Json.Serialization;
using CounterStrikeSharp.API.Core;

namespace EventsManager.plugin;

public class EventsManagerConfig : BasePluginConfig
{
    [JsonPropertyName("WEB_SERVER_URL")] public string? WebServerUrl { get; set; } = "http://localhost";
    [JsonPropertyName("WEB_SERVER_PORT")] public string? WebServerPort { get; set; } = "8000";
}