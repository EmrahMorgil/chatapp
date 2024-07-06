using Server.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Image { get; set; } = null!;
} 
