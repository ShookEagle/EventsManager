namespace EventsManager.plugin.models;

public class CommandPack
{
    public string Description { get; set; } = string.Empty;
    public List<string> OnExecCmds { get; set; } = [];
    public List<string> OffExecCmds { get; set; } = [];
}