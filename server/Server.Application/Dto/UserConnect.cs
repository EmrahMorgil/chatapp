using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Dto
{
    public class UserConnect
    {
        public List<string> usersIds { get; set; }
        public string message { get; set; }
        public string lastUserId { get; set; }
    }
}
