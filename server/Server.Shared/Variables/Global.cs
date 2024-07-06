namespace Server.Shared.Variables;

public static class Global
{
    public static string ConnectionString { get; set; } = null!;
    public static Guid UserId { get; set; }
    public static string Email { get; set; } = null!;
    public static string Secret { get; set; } = null!;
    public static string ValidIssuer { get; set; } = null!;
    public static string ValidAudience { get; set; } = null!;
    public static int ValidityPeriod { get; set; }
}