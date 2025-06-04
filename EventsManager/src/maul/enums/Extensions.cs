namespace EventsManager.maul.enums;

public static class Extensions
{
    public static string ToString(this MaulPermission permission)
    {
        return permission switch
        {
            MaulPermission.E => "e",
            MaulPermission.EG => "eG",
            MaulPermission.EGO => "eGO",
            MaulPermission.Advisor => "advisor",
            MaulPermission.Manager => "manager",
            MaulPermission.SeniorManager => "senior-manager",
            MaulPermission.CommunityManager => "community-manager",
            MaulPermission.Director => "director",
            MaulPermission.Executive => "executive",
            _ => "pub"
        };
    }
}