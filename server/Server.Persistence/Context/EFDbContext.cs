using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Persistence.Context
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(DbContextOptions options) : base(options) { }
    }
}
