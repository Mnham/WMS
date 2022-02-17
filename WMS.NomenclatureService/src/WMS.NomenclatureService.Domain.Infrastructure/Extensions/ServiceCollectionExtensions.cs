using Dapper;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using WMS.Microservice.Domain.Infrastructure.Configuration;
using WMS.Microservice.Domain.Infrastructure.Repositories.Infrastructure;
using WMS.Microservice.Domain.Infrastructure.Repositories.Infrastructure.Contracts;
using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureAggregate;
using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureTypeAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Repositories.Implementation;

namespace WMS.NomenclatureService.Domain.Infrastructure.Extensions
{
    /// <summary>
    /// Представляет методы расширения для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавляет компоненты для работы с базами данных.
        /// </summary>
        public static IServiceCollection AddDatabaseComponents(this IServiceCollection services, IConfiguration configuration) =>
            services.Configure<DatabaseConnectionOptions>(configuration.GetSection(nameof(DatabaseConnectionOptions)))
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IChangeTracker, ChangeTracker>()
                .AddScoped<IQueryExecutor, QueryExecutor>();

        /// <summary>
        /// Регистрирует репозитории.
        /// </summary>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            services.AddScoped<INomenclatureRepository, NomenclatureRepository>();
            services.AddScoped<INomenclatureTypeRepository, NomenclatureTypeRepository>();

            return services;
        }
    }
}