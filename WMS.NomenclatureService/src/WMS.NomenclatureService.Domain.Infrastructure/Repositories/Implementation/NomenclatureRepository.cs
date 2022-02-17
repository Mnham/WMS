using Dapper;

using Microsoft.Extensions.Options;

using Npgsql;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using WMS.Microservice.Domain.Infrastructure.Configuration;
using WMS.Microservice.Domain.Infrastructure.Repositories.Infrastructure;
using WMS.Microservice.Domain.Infrastructure.Repositories.Infrastructure.Contracts;
using WMS.Microservice.Extensions;
using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Helpers;
using WMS.NomenclatureService.Domain.Infrastructure.Models;

using static Dapper.SqlMapper;

namespace WMS.NomenclatureService.Domain.Infrastructure.Repositories.Implementation
{
    /// <summary>
    /// Представляет репозиторий номенклатуры.
    /// </summary>
    public class NomenclatureRepository : INomenclatureRepository
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
        /// Обработчик запроса к базе данных.
        /// </summary>
        private readonly IQueryExecutor _queryExecutor;

        /// <summary>
        /// Создает экземпляр класса <see cref="NomenclatureRepository"/>.
        /// </summary>
        public NomenclatureRepository(IOptions<DatabaseConnectionOptions> options, IQueryExecutor queryExecutor)
        {
            _options = options.Value;
            _queryExecutor = queryExecutor;
        }

        /// <summary>
        /// Выполняет поиск.
        /// </summary>
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

        /// <summary>
        /// Добавляет номенклатуру.
        /// </summary>
        public async Task<Nomenclature> Insert(Nomenclature itemToInsert, CancellationToken cancellationToken)
        {
            const string sql = @"
                INSERT INTO nomenclature (
                    name
                    ,nomenclature_type_id
                    ,length
                    ,width
                    ,height
                    ,weight
                )
                VALUES (
                    @Name
                    ,@NomenclatureTypeId
                    ,@Length
                    ,@Width
                    ,@Height
                    ,@Weight
                )
                RETURNING id ;";

            var parameters = new
            {
                Name = itemToInsert.Name,
                NomenclatureTypeId = itemToInsert.Type.Id,
                Length = itemToInsert.Length,
                Width = itemToInsert.Width,
                Height = itemToInsert.Height,
                Weight = itemToInsert.Weight
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
        /// Обновляет номенклатуру.
        /// </summary>
        public async Task<Nomenclature> Update(Nomenclature itemToUpdate, CancellationToken cancellationToken)
        {
            const string sql = @"
                UPDATE nomenclature
                SET name = @Name
                    ,nomenclature_type_id = @NomenclatureTypeId
                    ,length = @Length
                    ,width = @Width
                    ,height = @Height
                    ,weight = @Weight
                WHERE id = @NomenclatureId ;";

            var parameters = new
            {
                NomenclatureId = itemToUpdate.Id,
                Name = itemToUpdate.Name,
                NomenclatureTypeId = itemToUpdate.Type.Id,
                Length = itemToUpdate.Length,
                Width = itemToUpdate.Width,
                Height = itemToUpdate.Height,
                Weight = itemToUpdate.Weight
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