using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;
using WMS.EmployeeService.Domain.Infrastructure.Models;
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
        /// Создает экземпляр класса <see cref="EmployeeSessionRepository"/>.
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
        /// Возвращает сессию по идентификатору.
        /// </summary>
        public async Task<EmployeeSession> GetById(long id, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT *
                FROM nomenclature
                WHERE id = @Id
                LIMIT 1 ;";

            var parameter = new
            {
                Id = id
            };

            using NpgsqlConnection connection = new(_options.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            CommandDefinition command = new(
                commandText: sql,
                parameters: parameter,
                commandTimeout: TIMEOUT,
                cancellationToken: cancellationToken);

            return await _queryExecutor.Execute(async () =>
            {
                EmployeeSessionDto employee = await connection.QuerySingleOrDefaultAsync<EmployeeSessionDto>(command);
                return EmployeeSessionMapper.DtoToEntity(employee);
            });
        }

        /// <summary>
        /// Обновлет сессию.
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