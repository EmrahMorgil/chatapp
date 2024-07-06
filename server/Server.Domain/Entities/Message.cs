using Server.Domain.Common;

namespace Server.Domain.Entities;

public class Message: BaseEntity
{
    public Guid SenderId { get; set; }
    public string Content { get; set; } = null!;
    public string Room { get; set; } = null!;
}
