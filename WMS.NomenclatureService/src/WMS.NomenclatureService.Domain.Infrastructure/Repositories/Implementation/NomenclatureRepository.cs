using Dapper;

using Microsoft.Extensions.Options;

using Npgsql;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using WMS.ClassLibrary.Domain.Infrastructure.Configuration;
using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure;
using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Contracts;
using WMS.ClassLibrary.Extensions;
using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Helpers;
using WMS.NomenclatureService.Domain.Infrastructure.Models;

using static Dapper.SqlMapper;

namespace WMS.NomenclatureService.Domain.Infrastructure.Repositories.Implementation
{
    public class NomenclatureRepository : INomenclatureRepository
    {
        private const int TIMEOUT = 5;
        private readonly DatabaseConnectionOptions _options;
        private readonly IQueryExecutor _queryExecutor;

        public NomenclatureRepository(IOptions<DatabaseConnectionOptions> options, IQueryExecutor queryExecutor)
        {
            _options = options.Value;
            _queryExecutor = queryExecutor;
        }

        public async Task<IReadOnlyCollection<Nomenclature>> Search(NomenclatureFilter filter, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT *
                FROM nomenclature
                INNER JOIN nomenclature_type ON nomenclature.nomenclature_type_id = nomenclature_type.id
                /**where**/;";

            SqlBuilder builder = new();
            SqlBuilder.Template select = builder.AddTemplate(sql);
            DynamicParameters parameter = new();
            IReadOnlyList<FilterParameter> filters = FilterParameter.GetFilters(filter);

            foreach (FilterParameter f in filters)
            {
                parameter.Add(f.ParameterName, f.Value);
                builder.Where($"{f.SqlField} {f.SqlOperator} @{f.ParameterName}");
            }

            using NpgsqlConnection connection = new(_options.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            CommandDefinition commandDefinition = new(
                select.RawSql,
                parameters: parameter,
                commandTimeout: TIMEOUT,
                cancellationToken: cancellationToken);

            IEnumerable<Nomenclature> result = await _queryExecutor.Execute(async () =>
            {
                IEnumerable<NomenclatureDto> nomenclatures = await connection.QueryAsync<NomenclatureDto, NomenclatureTypeDto, NomenclatureDto>(
                    commandDefinition,
                    (nomenclature, nomenclatureType) =>
                    {
                        nomenclature.Type = nomenclatureType;

                        return nomenclature;
                    },
                    splitOn: "id");

                return nomenclatures.Distinct().Map(NomenclatureMapper.DtoToEntity);
            });

            return result.ToList();
        }

        public Task<Nomenclature> Insert(Nomenclature nomenclature, CancellationToken cancellationToken) => throw new System.NotImplementedException();

        public Task<Nomenclature> Update(Nomenclature nomenclature, CancellationToken cancellationToken) => throw new System.NotImplementedException();
    }
}