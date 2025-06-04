using System.Text;
using System.Text.Json;
using EventsManager.api.plugin;
using EventsManager.api.plugin.services;

namespace EventsManager.plugin.services;

public class WebService : IWebService
{
    private readonly HttpClient _http;
    private readonly IEventsManager plugin;
    
    public WebService(HttpClient httpClient, IEventsManager plugin)
    {
        this.plugin = plugin;
        var settings = plugin.Config;
        _http = httpClient;
        _http.BaseAddress = new Uri($"{plugin.Config.WebServerUrl}:{plugin.Config.WebServerPort}/api/");
    }
    
    public async Task<T?> GetAsync<T>(string endpoint)
    {
        try
        {
            var response = await _http.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode) return default;
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json);
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
}