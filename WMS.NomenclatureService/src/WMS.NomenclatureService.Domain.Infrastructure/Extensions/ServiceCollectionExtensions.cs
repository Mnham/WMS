using Dapper;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using WMS.ClassLibrary.Domain.Infrastructure.Configuration;
using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure;
using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Contracts;
using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureAggregate;
using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureTypeAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Repositories.Implementation;

namespace WMS.NomenclatureService.Domain.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseComponents(this IServiceCollection services, IConfiguration configuration) =>
            services.Configure<DatabaseConnectionOptions>(configuration.GetSection(nameof(DatabaseConnectionOptions)))
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IChangeTracker, ChangeTracker>()
                .AddScoped<IQueryExecutor, QueryExecutor>();

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            services.AddScoped<INomenclatureRepository, NomenclatureRepository>();
            services.AddScoped<INomenclatureTypeRepository, NomenclatureTypeRepository>();

            return services;
        }
    }
}