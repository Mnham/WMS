using Dapper;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using WMS.ClassLibrary.Domain.Infrastructure.Configuration;
using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure;
using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Contracts;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Repositories.Implementation;

namespace WMS.EmployeeService.Domain.Infrastructure.Extensions
{
    /// <summary>
    /// Предоставляет методы расширения для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавляет компонеты для работы с базами данных.
        /// </summary>
        public static IServiceCollection AddDatabaseComponents(this IServiceCollection services, IConfiguration configuration) =>
            services.Configure<DatabaseConnectionOptions>(configuration.GetSection(nameof(DatabaseConnectionOptions)))
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IChangeTracker, ChangeTracker>()
                .AddScoped<IQueryExecutor, QueryExecutor>();

        /// <summary>
        /// Выполняет регистрацию репозиториев в <see cref="IServiceCollection"/>.
        /// </summary>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeSessionRepository, EmployeeSessionRepository>();

            return services;
        }
    }
}