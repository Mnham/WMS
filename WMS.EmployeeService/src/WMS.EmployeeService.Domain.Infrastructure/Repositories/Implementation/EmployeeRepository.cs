using Dapper;

using Microsoft.Extensions.Options;

using Npgsql;

using WMS.ClassLibrary.Domain.Infrastructure.Configuration;
using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure;
using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Contracts;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Models;
using static Dapper.SqlMapper;

namespace WMS.EmployeeService.Domain.Infrastructure.Repositories.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private const int TIMEOUT = 5;
        private readonly DatabaseConnectionOptions _options;
        private readonly IQueryExecutor _queryExecutor;

        public EmployeeRepository(IOptions<DatabaseConnectionOptions> options, IQueryExecutor queryExecutor)
        {
            _options = options.Value;
            _queryExecutor = queryExecutor;
        }

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

            CommandDefinition commandDefinition = new(
                commandText: sql,
                parameters: parameters,
                commandTimeout: TIMEOUT,
                cancellationToken: cancellationToken);

            return await _queryExecutor.Execute(itemToInsert, async () =>
            {
                long id = await connection.QuerySingleAsync<long>(commandDefinition);
                itemToInsert.SetId(id);
            });
        }

        public async Task<IReadOnlyCollection<Employee>> Search(EmployeeFilter filter, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT *
                FROM employee
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
                commandText: select.RawSql,
                parameters: parameter,
                commandTimeout: TIMEOUT,
                cancellationToken: cancellationToken);

            IEnumerable<Employee> result = await _queryExecutor.Execute(async ()
                    => await connection.QueryAsync<Employee>(commandDefinition));

            return result.ToList();
        }

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

            CommandDefinition commandDefinition = new(
                commandText: sql,
                parameters: parameters,
                commandTimeout: TIMEOUT,
                cancellationToken: cancellationToken);

            return await _queryExecutor.Execute(itemToUpdate, async () => await connection.ExecuteAsync(commandDefinition));
        }
    }
}