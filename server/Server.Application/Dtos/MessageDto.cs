namespace Server.Application.Dto;

public class MessageDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public UserDto SenderUser { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string Room { get; set; } = null!;
}
