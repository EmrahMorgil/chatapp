using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Dto
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid SenderId { get; set; }
        public string SenderName { get; set; }
        public string Content { get; set; } = null!;
        public string Room { get; set; } = null!;
    }
}
