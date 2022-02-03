using MediatR;

using WMS.ClassLibrary.Extensions;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;

namespace WMS.EmployeeService.Domain.Infrastructure.Handlers.EmployeeAggregate
{
    /// <summary>
    /// Представляет обработчик команды поиска данных сотрудника.
    /// </summary>
    public class SearchEmployeeQueryHandler : IRequestHandler<SearchEmployeeQuery, SearchEmployeeQueryResponse>
    {
        /// <summary>
        /// Предоставляет экземпляр репозитория данных сотрудников.
        /// </summary>
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="SearchEmployeeQueryHandler"/>.
        /// </summary>
        public SearchEmployeeQueryHandler(IEmployeeRepository employeeRepository) => _employeeRepository = employeeRepository;

        /// <summary>
        /// Выполняет обработку команды поиска данных сотрудника.
        /// </summary>
        public async Task<SearchEmployeeQueryResponse> Handle(SearchEmployeeQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<Employee> employees = await _employeeRepository.Search(
                new EmployeeFilter
                {
                    EmployeeId = request.EmployeeId,
                    EmployeeName = request.EmployeeName
                }, cancellationToken);

            return new SearchEmployeeQueryResponse
            {
                Items = employees.Map(EmployeeMapper.EntityToDto).ToList()
            };
        }
    }
}