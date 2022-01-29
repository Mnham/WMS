using Dapper;

using Microsoft.Extensions.Options;

using Npgsql;

using WMS.ClassLibrary.Domain.Infrastructure.Configuration;
using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Contracts;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate;

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

        public async Task<IReadOnlyCollection<Employee>> Search(EmployeeFilter request, CancellationToken cancellationToken) => throw new Exception();

        public async Task<Employee> Update(Employee employee, CancellationToken cancellationToken) => throw new Exception();
    }
}