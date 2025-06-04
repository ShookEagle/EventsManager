namespace EventsManager.plugin.models;

public class MapGroup : List<MapInfo> { }

public abstract class MapInfo
{
    public string Name { get; set; } = string.Empty;
    public string WorkshopId { get; set; } = string.Empty;
}