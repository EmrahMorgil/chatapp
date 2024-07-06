using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Server.Application.Interfaces.Repository;
using Server.Persistence.Context;
using Server.Persistence.Repositories;
using System.Reflection;

namespace Server.Persistence;
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
                .WithGlobalConnectionString(Configuration.GetSettings<string>("ConnectionStrings:ConnectionString"))
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());
    }
}
