using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Common
{
    public class BaseEntity
    {
        public Guid id { get; set; }
        public DateTime createdDate { get; set; }
    }
}
