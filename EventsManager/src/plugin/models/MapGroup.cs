namespace EventsManager.plugin.models;

public class MapGroup : List<MapInfo>
{
    public MapGroup() { }
}

public class MapInfo
{
    public string Name { get; set; } = string.Empty;
    public string? WorkshopId { get; set; } = string.Empty;
}