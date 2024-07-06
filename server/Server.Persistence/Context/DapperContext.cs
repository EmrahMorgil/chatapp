using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Server.Application.Variables;
using System.Data;

namespace Server.Persistence.Context;

public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = Global.ConnectionString;
    }
    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}
