using FluentMigrator.Runner;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Npgsql;

using System;
using System.IO;
using System.Linq;

namespace WMS.NomenclatureService.Migrator
{
    public class Program
    {
        private static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            string connectionString = configuration.GetSection("DatabaseConnectionOptions:ConnectionString").Get<string>();
            IServiceCollection services = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(
                    rb => rb
                        .AddPostgres()
                        .WithGlobalConnectionString(connectionString)
                        .ScanIn(typeof(Program).Assembly)
                        .For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());

            ServiceProvider serviceProvider = services.BuildServiceProvider(false);

            using (serviceProvider.CreateScope())
            {
                IMigrationRunner runner = serviceProvider.GetRequiredService<IMigrationRunner>();
                if (args.Contains("--dryrun"))
                {
                    runner.ListMigrations();
                }
                else
                {
                    runner.MigrateUp();
                }

                using NpgsqlConnection connection = new(connectionString);
                connection.Open();
                connection.ReloadTypes();
            }
        }
    }
}