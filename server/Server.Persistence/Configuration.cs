using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Persistence
{
    static class Configuration
    {
        static public string ConnectionString
        {
            get
            {
                //appsettings.json içerisinden connection stringi çekmek için bunu yapıyoruz. 2 tane paket yükledik.
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Server.WebApi"));
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("DefaultConnection");
            }
        }
    }
}
