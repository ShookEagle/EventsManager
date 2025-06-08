using CounterStrikeSharp.API.Modules.Utils;

namespace EventsManager.maul.enums;

public enum MaulPermission {
    None = 0,
    E = 10,
    EG = 20,
    EGO = 30,
    Advisor = 50,
    Manager = 60,
    SeniorManager = 70,
    CommunityManager = 90,
    Director = 91,
    Executive = 92,
    Root = 100
}

public static class MaulPermissionExtensions {
    public static char GetChatColor(this MaulPermission perm) {
        return perm switch {
            MaulPermission.None          => ChatColors.Default,
            MaulPermission.E             => ChatColors.Green,
            MaulPermission.EG            => ChatColors.Green,
            MaulPermission.EGO           => ChatColors.LightBlue,
            MaulPermission.Advisor       => ChatColors.DarkBlue,
            MaulPermission.Manager       => ChatColors.LightRed,
            MaulPermission.SeniorManager => ChatColors.DarkRed,
            _                            => ChatColors.Magenta,
        };
    }
}
