using Server.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Domain.Entities;

public class User : BaseEntity
{
    [Column("email")]
    public string Email { get; set; } = null!;
    [Column("name")]
    public string Name { get; set; } = null!;
    [Column("password")]
    public string Password { get; set; } = null!;
    [Column("image")]
    public string Image { get; set; } = null!;
} 
