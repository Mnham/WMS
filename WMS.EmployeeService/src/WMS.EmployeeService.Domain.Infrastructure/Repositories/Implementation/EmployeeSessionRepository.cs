using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate;
using WMS.Microservice.Domain.Infrastructure.Configuration;
using WMS.Microservice.Domain.Infrastructure.Repositories.Infrastructure;
using WMS.Microservice.Domain.Infrastructure.Repositories.Infrastructure.Contracts;

namespace WMS.EmployeeService.Domain.Infrastructure.Repositories.Implementation
{
    /// <summary>
    /// Представляет репозиторий сессий.
    /// </summary>
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
        /// Инициализирует новый экземпляр <see cref="EmployeeSessionRepository"/>.
        /// </summary>
        public EmployeeSessionRepository(IOptions<DatabaseConnectionOptions> options, IQueryExecutor queryExecutor)
        {
            _options = options.Value;
            _queryExecutor = queryExecutor;
        }

        /// <summary>
        /// Добавляет сессию.
        /// </summary>
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

        /// <summary>
        /// Возвращает данные сессии по идентификатору.
        /// </summary>
        public async Task<EmployeeSession> GetById(IntIdModel id, CancellationToken cancellationToken)
        {
            const string sql = @"
                TOP (1)
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

            return await _queryExecutor.Execute(async () => await connection.ExecuteScalarAsync<EmployeeSession>(command));
        }

        /// <summary>
        /// Обновлет данные сессии.
        /// </summary>
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