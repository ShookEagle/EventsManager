namespace EventsManager.plugin.models;

public abstract class GameMode
{
    public string MapGroup { get; set; } = "Active";
    public string Description { get; set; } = "";
    public string Tags { get; set; } = "";
    public List<string> Plugins { get; set; } = [];
    public List<string> CommandPacks { get; set; } = [];
    public GameModeSettings Settings { get; set; } = new();
}

public class GameModeSettings
{
    public Dictionary<string, object>? Generic { get; set; }
    public Dictionary<string, object>? Roundflow { get; set; }
    public Dictionary<string, object>? Bots { get; set; }
    public Dictionary<string, object>? Movement { get; set; }
    public Dictionary<string, object>? Weapons { get; set; }
    public Dictionary<string, object>? Players { get; set; }
    public Dictionary<string, object>? Communication { get; set; }
    public List<string>? CustomCommands { get; set; }
}