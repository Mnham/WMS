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
    /// <summary>
    /// Представляет репозиторий типа номенклатуры.
    /// </summary>
    public class NomenclatureTypeRepository : INomenclatureTypeRepository
    {
        /// <summary>
        /// Таймаут подключения к базе данных.
        /// </summary>
        private const int TIMEOUT = 5;

        /// <summary>
        /// Настройки подключения к базе данных.
        /// </summary>
        private readonly DatabaseConnectionOptions _options;

        /// <summary>
        /// Экземпляр класса для обработки запроса к базе данных.
        /// </summary>
        private readonly IQueryExecutor _queryExecutor;

        /// <summary>
        /// Создает экземпляр класса <see cref="NomenclatureTypeRepository"/>.
        /// </summary>
        public NomenclatureTypeRepository(IOptions<DatabaseConnectionOptions> options, IQueryExecutor queryExecutor)
        {
            _options = options.Value;
            _queryExecutor = queryExecutor;
        }

        /// <summary>
        /// Возвращает все типы номенклатур.
        /// </summary>
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

        /// <summary>
        /// Добавляет тип номенклатуры.
        /// </summary>
        public async Task<NomenclatureType> Insert(NomenclatureType itemToInsert, CancellationToken cancellationToken)
        {
            const string sql = @"
                INSERT INTO nomenclature_type (name)
                VALUES (@Name)
                RETURNING id ;";

            var parameters = new
            {
                Name = itemToInsert.Name
            };

            using NpgsqlConnection connection = new(_options.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            CommandDefinition commandDefinition = new(
                sql,
                parameters: parameters,
                commandTimeout: TIMEOUT,
                cancellationToken: cancellationToken);

            return await _queryExecutor.Execute(itemToInsert, async () =>
            {
                long id = await connection.QuerySingleAsync<long>(commandDefinition);
                itemToInsert.SetId(id);
            });
        }

        /// <summary>
        /// Обновляет тип номенклатуры.
        /// </summary>
        public async Task<NomenclatureType> Update(NomenclatureType itemToUpdate, CancellationToken cancellationToken)
        {
            const string sql = @"
                UPDATE nomenclature_type
                SET name = @Name
                WHERE id = @NomenclatureTypeId ;";

            var parameters = new
            {
                NomenclatureTypeId = itemToUpdate.Id,
                Name = itemToUpdate.Name,
            };

            using NpgsqlConnection connection = new(_options.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            CommandDefinition commandDefinition = new(
                sql,
                parameters: parameters,
                commandTimeout: TIMEOUT,
                cancellationToken: cancellationToken);

            return await _queryExecutor.Execute(itemToUpdate, async () => await connection.ExecuteAsync(commandDefinition));
        }
    }
}