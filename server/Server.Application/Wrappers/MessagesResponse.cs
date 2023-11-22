using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Wrappers
{
    public class MessagesResponse
    {
        public List<Message> messages { get; set; }
        public string? room { get; set; }
    }
}
