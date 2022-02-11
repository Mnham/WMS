using MediatR;

using WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;

namespace WMS.EmployeeService.Domain.Infrastructure.Handlers.EmployeeAggregate
{
    /// <summary>
    /// Представляет обработчик команды добавления данных сотрудника.
    /// </summary>
    public class InsertEmployeeQueryHandler : IRequestHandler<InsertEmployeeQuery, InsertEmployeeQueryResponse>
    {
        /// <summary>
        /// Предоставляет экземпляр репозитория данных сотрудников.
        /// </summary>
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="InsertEmployeeQueryHandler"/>.
        /// </summary>
        public InsertEmployeeQueryHandler(IEmployeeRepository employeeRepository) =>
            _employeeRepository = employeeRepository;

        /// <summary>
        /// Обрабатывает команду добавления данных сотрудника.
        /// </summary>
        public async Task<InsertEmployeeQueryResponse> Handle(InsertEmployeeQuery request, CancellationToken cancellationToken)
        {
            Employee employee = await _employeeRepository.Insert(
                EmployeeMapper.DtoToEntity(request.Employee),
                cancellationToken);

            return new InsertEmployeeQueryResponse()
            {
                Employee = EmployeeMapper.EntityToDto(employee)
            };
        }
    }
}