using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Common
{
    public class BaseEntity
    {
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
