using System.Windows.Input;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Capabilities;
using EventsManager.api.plugin;
using EventsManager.api.plugin.services;
using EventsManager.plugin.commands;
using EventsManager.plugin.services;
using MAULActainShared.plugin;
using Microsoft.Extensions.Logging;

namespace EventsManager.plugin;

public class EventsManager : BasePlugin, IEventsManager
{
    public override string ModuleName => "EventManager";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "ShookEagle";
    public override string ModuleDescription => "eGO Event Server Plugin hooked with Web Panel";

    private static PluginCapability<IActain>? ActainCapability { get; } = new("maulactain:core");
    private readonly Dictionary<string, Command> _commands = new();

    private IWebService? _webService;
    private IMapGroupService? _mapGroupService;
    private ICommandPackService? _commandPackService;
    private IGameModesService? _gameModesService;

#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
    public EventsManagerConfig? Config { get; set; }
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
    public void OnConfigParsed(EventsManagerConfig? config) { Config = config; }

    public BasePlugin GetBase() { return this; }
    public IActain GetActain() { return ActainCapability!.Get()!; }
    public IWebService GetWebService() { return _webService!; }
    public IMapGroupService GetMapGroupService() { return _mapGroupService!; }
    public ICommandPackService GetCommandPackService() { return _commandPackService!; }
    public IGameModesService GetGameModesService() { return _gameModesService!; }
    
    public override async void Load(bool hotReload)
    {
        _webService = new WebService(new HttpClient(), this);
        _mapGroupService = new MapGroupService(_webService, this);
        _commandPackService = new CommandPackService(_webService, this);
        _gameModesService = new GameModesService(this);

        await _mapGroupService.LoadAsync();
        await _commandPackService.LoadAsync();
        
        LoadCommands();
    }
    
    private void LoadCommands()
    {
        foreach (var command in _commands)
            AddCommand(command.Key, command.Value.Description ?? "No Description Provided", command.Value.OnCommand);
    }

}