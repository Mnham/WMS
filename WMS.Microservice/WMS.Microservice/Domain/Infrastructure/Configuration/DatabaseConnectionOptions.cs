namespace WMS.Microservice.Domain.Infrastructure.Configuration
{
    /// <summary>
    /// Представляет настройки подключения к базе данных.
    /// </summary>
    public sealed class DatabaseConnectionOptions
    {
        /// <summary>
        /// Строка подключения.
        /// </summary>
        public string ConnectionString { get; set; }
    }
}