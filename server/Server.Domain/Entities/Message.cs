using Server.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class Message : BaseEntity
    {
        public string? senderId { get; set; }
        public string? message { get; set; }
        public string? room { get; set; }
    }
}
