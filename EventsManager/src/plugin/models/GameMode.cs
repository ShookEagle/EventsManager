namespace EventsManager.plugin.models;

public class GameMode
{
    public string MapGroup { get; set; } = "Active";
    public string Description { get; set; } = "";
    public string Tags { get; set; } = "";
    public List<string>? Plugins { get; set; } = [];
    public List<string>? CommandPacks { get; set; } = [];
    public Dictionary<string, object?>? Settings { get; set; }
    
    public GameMode() {}
}