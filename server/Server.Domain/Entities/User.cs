using Server.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? email { get; set; }
        public string? name { get; set; }
        public string? password { get; set; }
        public string? image { get; set; }
    }
}
