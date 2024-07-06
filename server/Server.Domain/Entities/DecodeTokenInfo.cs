namespace Server.Domain.Entities;

public class DecodedTokenInfo
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = null!;
}

