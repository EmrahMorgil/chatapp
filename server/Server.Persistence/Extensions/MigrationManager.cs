using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Server.Persistence.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            string connectionString = "server=localhost;database=chatapp;trusted_connection=true;TrustServerCertificate=True;";
            string dbName = "chatapp";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                    SqlCommand createDbCommand = new SqlCommand($"CREATE DATABASE {dbName}", connection);
                    createDbCommand.ExecuteNonQuery();
            }



            using (var scope = host.Services.CreateScope())
            {
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                try
                {
                    migrationService.ListMigrations();
                    migrationService.MigrateUp();
                }
                catch
                {
                    throw;
                }
            }
            return host;
        }
    }
}
