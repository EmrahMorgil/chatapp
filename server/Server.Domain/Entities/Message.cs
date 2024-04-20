using Server.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class Message: BaseEntity
    {
        [Column("senderId")]
        public Guid SenderId { get; set; }
        [Column("content")]
        public string Content { get; set; } = null!;
        [Column("room")]
        public string Room { get; set; } = null!;
    }
}
