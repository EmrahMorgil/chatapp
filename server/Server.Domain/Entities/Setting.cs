using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class Settings
    {
        public string? secret { get; set; }
        public string? validIssuer { get; set; }
        public string? validAudience { get; set; }
        public int validityPeriod { get; set; }
    }
}
