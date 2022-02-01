using MediatR;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;

namespace WMS.EmployeeService.Domain.Infrastructure.Handlers.EmployeeAggregate
{
    /// <summary>
    /// Представляет обработчик команды обновления данных сотрудника.
    /// </summary>
    public class UpdateEmployeeQueryHandler : IRequestHandler<UpdateEmployeeQuery, UpdateEmployeeQueryResponse>
    {
        /// <summary>
        /// Предоставляет экземпляр репозитория данных сотрудников.
        /// </summary>
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="IEmployeeRepository"/>.
        /// </summary>
        /// <param name="employeeRepository"></param>
        public UpdateEmployeeQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Выполняет обработку команды обновления данных сотрудника.
        /// </summary>
        public async Task<UpdateEmployeeQueryResponse> Handle(UpdateEmployeeQuery request, CancellationToken cancellationToken)
        {
            Employee employee = await _employeeRepository.Update(
                EmployeeMapper.DtoToEntity(request.Employee),
                cancellationToken);

            return new UpdateEmployeeQueryResponse
            {
                Employee = EmployeeMapper.EntityToDto(employee)
            };
        }
    }
}