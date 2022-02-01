using FluentMigrator.Runner;

using Npgsql;

namespace WMS.EmployeeService.Migrator
{
    /// <summary>
    /// ѕредоставл€ет методы запуска миграции базы данных.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ¬ыполн€ет миграцию базы данных.
        /// </summary>
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