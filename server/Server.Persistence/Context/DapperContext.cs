using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Persistence.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = "Data Source=SQL5105.site4now.net;Initial Catalog=db_aa1e60_chatapp;User Id=db_aa1e60_chatapp_admin;Password=emrah123;";
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
