using Serilog;

namespace WMS.EmployeeService
{
    /// <summary>
    /// Представляет точку входа.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа.
        /// </summary>
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        /// <summary>
        /// Конфигурирует хост.
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
         Host.CreateDefaultBuilder(args)
             .UseSerilog((context, configuration) => configuration
                 .ReadFrom
                 .Configuration(context.Configuration)
                 .WriteTo.Console())
             .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}