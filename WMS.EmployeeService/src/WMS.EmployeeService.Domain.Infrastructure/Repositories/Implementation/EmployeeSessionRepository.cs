using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using WMS.ClassLibrary.Domain.Infrastructure.Configuration;
using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure;
using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Contracts;
using WMS.ClassLibrary.Extensions;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;
using WMS.EmployeeService.Domain.Infrastructure.Models;

namespace WMS.EmployeeService.Domain.Infrastructure.Repositories.Implementation
{
    public class EmployeeSessionRepository : IEmployeeSessionRepository
    {
        /// <summary>
        /// Предоставляет время ожидания подключения к базе данных.
        /// </summary>
        private const int TIMEOUT = 5;

        /// <summary>
        /// Предоставляет настройки подключения к базе жанных.
        /// </summary>
        private readonly DatabaseConnectionOptions _options;

        /// <summary>
        /// Предоставляет обработчик запроса.
        /// </summary>
        private readonly IQueryExecutor _queryExecutor;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="EmployeeRepository"/>.
        /// </summary>
        public EmployeeSessionRepository(IOptions<DatabaseConnectionOptions> options, IQueryExecutor queryExecutor)
        {
            _options = options.Value;
            _queryExecutor = queryExecutor;
        }

        public async Task<EmployeeSession> Insert(EmployeeSession itemToInsert, CancellationToken cancellationToken)
        {
            const string sql = @"
                INSERT INTO employee_service (
                    employee_id,
                    task_type_id,
                    equipment_id)
                VALUES (
                    @EmployeeId,
                    @TaskTypeId,
                    @EquipmentId)
                RETURNING id;";

            var parameters = new
            {
                EmployeeId = itemToInsert.EmployeeId,
                TaskTypeId = itemToInsert.TaskTypeId,
                EquipmentId = itemToInsert.EquipmentId
            };

            using NpgsqlConnection connection = new(_options.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            CommandDefinition command = new(
                commandText: sql,
                parameters: parameters,
                commandTimeout: TIMEOUT,
                cancellationToken: cancellationToken);

            return await _queryExecutor.Execute(itemToInsert, async () =>
            {
                long id = await connection.QuerySingleAsync<long>(command);
                itemToInsert.SetId(id);
            });
        }

        public async Task<IReadOnlyCollection<EmployeeSession>> GetById(IntIdModel id, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT *
                FROM nomenclature
                /**where**/;";

            SqlBuilder builder = new();
            SqlBuilder.Template select = builder.AddTemplate(sql);
            DynamicParameters parameter = new();
            IReadOnlyList<FilterParameter> filters = FilterParameter.GetFilters(id);

            foreach (FilterParameter f in filters)
            {
                parameter.Add(f.ParameterName, f.Value);
                builder.Where($"{f.SqlField} {f.SqlOperator} @{f.ParameterName}");
            }

            using NpgsqlConnection connection = new(_options.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            CommandDefinition command = new(
                select.RawSql,
                parameters: parameter,
                commandTimeout: TIMEOUT,
                cancellationToken: cancellationToken);

            IEnumerable<EmployeeSession> result = await _queryExecutor.Execute(async () =>
            {
                IEnumerable<EmployeeSessionDto> sessions = await connection.QueryAsync<EmployeeSessionDto>(command);
                return sessions.Distinct().Map(EmployeeSessionMapper.DtoToEntity);
            });

            return result.ToList();
        }

        public async Task<EmployeeSession> Update(EmployeeSession itemToUpdate, CancellationToken cancellationToken)
        {
            const string sql = @"
                UPDATE employee_service
                SET employee_id = @EmployeeId,
                    task_type_id = @TaskTypeId,
                    equipment_id = @EquipmentId
                WHERE id = @Id";

            var parameters = new
            {
                Id = itemToUpdate.Id,
                EmployeeId = itemToUpdate.EmployeeId,
                TaskTypeId = itemToUpdate.TaskTypeId,
                EquipmentId = itemToUpdate.EquipmentId
            };

            using NpgsqlConnection connection = new(_options.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            CommandDefinition command = new(
                commandText: sql,
                parameters: parameters,
                commandTimeout: TIMEOUT,
                cancellationToken: cancellationToken);

            return await _queryExecutor.Execute(itemToUpdate, async () => await connection.ExecuteAsync(command));
        }
    }
}