using Dapper;

using Microsoft.Extensions.Options;

using Npgsql;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using WMS.ClassLibrary.Domain.Infrastructure.Configuration;
using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Contracts;
using WMS.ClassLibrary.Extensions;
using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureTypeAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Helpers;
using WMS.NomenclatureService.Domain.Infrastructure.Models;

using static Dapper.SqlMapper;

namespace WMS.NomenclatureService.Domain.Infrastructure.Repositories.Implementation
{
    public class NomenclatureTypeRepository : INomenclatureTypeRepository
    {
        private const int TIMEOUT = 5;
        private readonly DatabaseConnectionOptions _options;
        private readonly IQueryExecutor _queryExecutor;

        public NomenclatureTypeRepository(IOptions<DatabaseConnectionOptions> options, IQueryExecutor queryExecutor)
        {
            _options = options.Value;
            _queryExecutor = queryExecutor;
        }

        public async Task<IReadOnlyCollection<NomenclatureType>> GetAll(CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT *
                FROM nomenclature_type;";

            using NpgsqlConnection connection = new(_options.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            CommandDefinition commandDefinition = new(
                sql,
                commandTimeout: TIMEOUT,
                cancellationToken: cancellationToken);

            IEnumerable<NomenclatureType> result = await _queryExecutor.Execute(async () =>
            {
                IEnumerable<NomenclatureTypeDto> nomenclatures = await connection.QueryAsync<NomenclatureTypeDto>(commandDefinition);

                return nomenclatures.Distinct().Map(NomenclatureTypeMapper.DtoToEntity);
            });

            return result.ToList();
        }

        public Task<NomenclatureType> Insert(NomenclatureType nomenclatureType, CancellationToken cancellationToken) => throw new System.NotImplementedException();

        public Task<NomenclatureType> Update(NomenclatureType nomenclatureType, CancellationToken cancellationToken) => throw new System.NotImplementedException();
    }
}