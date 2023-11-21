using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Server.Application.Interfaces.Repository;
using Server.Persistence.Context;
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
            //services.AddDbContext<EFDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));

            services.AddSingleton<DapperContext>();
            //services.AddSingleton<Database>();

            services.AddLogging(c => c.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(c => c
                    .AddSqlServer2012()
                    .WithGlobalConnectionString(Configuration.ConnectionString)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());
        }
    }
}
