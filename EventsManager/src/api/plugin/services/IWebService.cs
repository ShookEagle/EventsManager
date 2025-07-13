namespace EventsManager.api.plugin.services;

public interface IWebService
{
    Task<T?> GetAsync<T>(string endpoint);
    Task<bool> PostAsync<T>(string endpoint, T data);
    void Connect();
}