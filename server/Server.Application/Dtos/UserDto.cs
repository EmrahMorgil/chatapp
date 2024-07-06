namespace Server.Application.Dto;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Image { get; set; } = null!;
}
