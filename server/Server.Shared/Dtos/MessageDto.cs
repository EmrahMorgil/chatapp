namespace Server.Shared.Dtos;

public class MessageDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Content { get; set; } = null!;
    public string Room { get; set; } = null!;
    public Guid UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string UserImage { get; set; } = null!;
}
