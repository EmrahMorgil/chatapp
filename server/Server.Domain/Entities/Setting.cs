namespace Server.Domain.Entities;

public class Settings
{
    public string Secret { get; set; } = null!;
    public string ValidIssuer { get; set; } = null!;
    public string ValidAudience { get; set; } = null!;
    public int ValidityPeriod { get; set; }
}
