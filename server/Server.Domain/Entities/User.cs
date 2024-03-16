using Server.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
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
}
