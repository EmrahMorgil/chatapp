using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Server.Persistence.Context;

public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = Configuration.GetSettings<string>("ConnectionStrings:ConnectionString");
    }
    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}
