using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Capabilities;
using EventsManager.api.plugin;
using EventsManager.api.plugin.services;
using EventsManager.plugin.commands;
using EventsManager.plugin.services;
using MAULActainShared.plugin;


namespace EventsManager.plugin;

public class EventsManager : BasePlugin, IEventsManager
{
    public override string ModuleName => "EventsManager";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "ShookEagle";
    public override string ModuleDescription => "eGO Event Server Plugin hooked with Web Panel";

    private static PluginCapability<IActain>? ActainCapability { get; } = new("maulactain:core");
    private readonly Dictionary<string, Command> _commands = new();

    private IWebService? _webService;
#pragma warning disable CA1859
    private IMapGroupService? _mapGroupService;
    private ICommandPackService? _commandPackService;
    private IGameModeService? _gameModesService;
    private IServerStateService? _serverStateService;
    private IPlayerStateService? _playerStateService;
#pragma warning restore CA1859
    private IAnnouncerService? _announcerService;
    private ILoggerService? _loggerService;

#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
    public EventsManagerConfig? Config { get; set; }
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
    public void OnConfigParsed(EventsManagerConfig? config) { Config = config; }

    public BasePlugin GetBase() { return this; }
    public IActain GetActain() { return ActainCapability!.Get()!; }
    public IWebService GetWebService() { return _webService!; }
    public IMapGroupService GetMapGroupService() { return _mapGroupService!; }
    public ICommandPackService GetCommandPackService() { return _commandPackService!; }
    public IGameModeService GetGameModesService() { return _gameModesService!; }
    public IServerStateService GetServerStateService() { return _serverStateService!; }
    public IPlayerStateService GetPlayerStateService() { return _playerStateService!;}
    public IAnnouncerService GetAnnouncerService() { return _announcerService!; }
    public ILoggerService GetLoggerService() { return _loggerService!; }
    
    public override void Load(bool hotReload)
    {
        _webService         = new WebService(new HttpClient(), this);
        _mapGroupService    = new MapGroupService(_webService, this);
        _commandPackService = new CommandPackService(_webService, this);
        _gameModesService   = new GameModeService(_webService, this);
        _serverStateService = new ServerStateService(_webService, this);
        _playerStateService = new PlayerStateService(_webService, this);
        _announcerService   = new AnnouncerService(this);
        _loggerService      = new LoggerService(this);

        LoadCommands();

        foreach (var player in Utilities.GetPlayers()) _playerStateService.GetOrCreate(player);
        
        _mapGroupService.LoadAsync().GetAwaiter().GetResult();
        _commandPackService.LoadAsync().GetAwaiter().GetResult();
        _gameModesService.LoadAsync().GetAwaiter().GetResult();
        _serverStateService.PushInitialStateAsync().GetAwaiter().GetResult();
        _playerStateService.PushAsync().GetAwaiter().GetResult();
    }
    
    private void LoadCommands()
    {
        _commands.Add("css_logs",   new LogsCmd(this));
        _commands.Add("css_ec",     new EcMenuCmd(this));
        
        foreach (var command in _commands)
            AddCommand(command.Key, command.Value.Description ?? "No Description Provided", command.Value.OnCommand);
    }

}