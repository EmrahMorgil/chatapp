using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Dto
{
    public class UserViewDto
    {
        public Guid id { get; set; }
        public string? name { get; set; }
        public string? image { get; set; }
    }
}
