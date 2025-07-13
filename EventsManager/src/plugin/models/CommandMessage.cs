namespace EventsManager.plugin.models;

public class CommandMessage {
  public string? Action { get; set; }
  public Dictionary<string, string?>? Params { get; init; }
  public long? Timestamp { get; init; }
}