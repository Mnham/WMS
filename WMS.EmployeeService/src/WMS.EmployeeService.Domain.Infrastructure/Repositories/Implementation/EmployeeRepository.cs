﻿using Dapper;

using Microsoft.Extensions.Options;

using Npgsql;

using WMS.ClassLibrary.Domain.Infrastructure.Configuration;
using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure;
using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Contracts;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate;

using static Dapper.SqlMapper;

namespace WMS.EmployeeService.Domain.Infrastructure.Repositories.Implementation
{
    /// <summary>
    /// Представляет репозиторий данных сотрудников.
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
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
        public EmployeeRepository(IOptions<DatabaseConnectionOptions> options, IQueryExecutor queryExecutor)
        {
            _options = options.Value;
            _queryExecutor = queryExecutor;
        }

        // TODO: документация
        public async Task<Employee> Insert(Employee itemToInsert, CancellationToken cancellationToken)
        {
            const string sql = @"
                INSERT INTO employee (name)
                VALUES (@Name)
                RETURNING id;";

            var parameters = new
            {
                Name = itemToInsert.Name
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

        // TODO: документация
        public async Task<IReadOnlyCollection<Employee>> Search(EmployeeFilter filter, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT *
                FROM employee
                /**where**/;";

            SqlBuilder builder = new();
            SqlBuilder.Template querySelect = builder.AddTemplate(sql);
            DynamicParameters parameter = new();
            IReadOnlyList<FilterParameter> filters = FilterParameter.GetFilters(filter);

            foreach (FilterParameter filterItem in filters)
            {
                parameter.Add(filterItem.ParameterName, filterItem.Value);
                builder.Where($"{filterItem.SqlField} {filterItem.SqlOperator} @{filterItem.ParameterName}");
            }

            using NpgsqlConnection connection = new(_options.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            CommandDefinition command = new(
                commandText: querySelect.RawSql,
                parameters: parameter,
                commandTimeout: TIMEOUT,
                cancellationToken: cancellationToken);

            IEnumerable<Employee> result = await _queryExecutor.Execute(async () =>
                await connection.QueryAsync<Employee>(command));

            return result.ToList();
        }

        // TODO: документация
        public async Task<Employee> Update(Employee itemToUpdate, CancellationToken cancellationToken)
        {
            const string sql = @"
                UPDATE employee
                SET name = @Name
                WHERE id = @EmployeeId;";

            var parameters = new
            {
                Name = itemToUpdate.Name,
                EmployeeId = itemToUpdate.Id
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