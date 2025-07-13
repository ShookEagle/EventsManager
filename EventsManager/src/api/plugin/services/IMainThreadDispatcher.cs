namespace EventsManager.api.plugin.services;

public interface IMainThreadDispatcher {
  void Enqueue(Action? action);
  void RunPending();
}