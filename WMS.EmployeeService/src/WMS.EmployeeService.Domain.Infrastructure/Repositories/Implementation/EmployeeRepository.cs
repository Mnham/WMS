using Dapper;

using Microsoft.Extensions.Options;

using Npgsql;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;
using WMS.EmployeeService.Domain.Infrastructure.Models;
using WMS.Microservice.Domain.Infrastructure.Configuration;
using WMS.Microservice.Domain.Infrastructure.Repositories.Infrastructure;
using WMS.Microservice.Domain.Infrastructure.Repositories.Infrastructure.Contracts;
using WMS.Microservice.Extensions;

namespace WMS.EmployeeService.Domain.Infrastructure.Repositories.Implementation
{
    /// <summary>
    /// Представляет репозиторий работников.
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
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
        /// Создает экземпляр класса <see cref="EmployeeRepository"/>.
        /// </summary>
        public EmployeeRepository(IOptions<DatabaseConnectionOptions> options, IQueryExecutor queryExecutor)
        {
            _options = options.Value;
            _queryExecutor = queryExecutor;
        }

        /// <summary>
        /// Добавляет работника.
        /// </summary>
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

        /// <summary>
        /// Выполняет поиск.
        /// </summary>
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
            {
                IEnumerable<EmployeeDto> employees = await connection.QueryAsync<EmployeeDto>(command);
                
                return employees.Distinct().Map(EmployeeMapper.DtoToEntity);
            });

            return result.ToList();
        }

        /// <summary>
        /// Обновляет данные работника.
        /// </summary>
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
