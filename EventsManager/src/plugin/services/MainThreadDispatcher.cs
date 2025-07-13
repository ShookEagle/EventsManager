using System.Collections.Concurrent;
using EventsManager.api.plugin.services;

namespace EventsManager.plugin.services;

public class MainThreadDispatcher : IMainThreadDispatcher {
  private readonly ConcurrentQueue<Action?> _actions = new();
  
  public void Enqueue(Action? action)
  {
    if (action != null)
      _actions.Enqueue(action);
  }

  public void RunPending()
  {
    while (_actions.TryDequeue(out var action))
    {
      try
      {
        action?.Invoke();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"[Dispatcher] Exception: {ex}");
      }
    }
  }
}