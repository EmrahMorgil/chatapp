using Server.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class Message
    {
        [Key]
        public int id { get; set; }
        public Guid senderId { get; set; }
        public string? message { get; set; }
        public string? room { get; set; }
        public DateTime createdDate { get; set; }
        public string? senderUserName { get; set; }
    }
}
