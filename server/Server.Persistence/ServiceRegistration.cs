using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Server.Application.Interfaces.Repository;
using Server.Persistence.Context;
using Server.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddLogging(c => c.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(c => c
                    .AddSqlServer2012()
                    .WithGlobalConnectionString("server=localhost;database=chatapp;trusted_connection=true;TrustServerCertificate=True;")
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());
        }
    }
}
