using MediatR;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Models;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate
{
    /// <summary>
    /// Представляет команду обновления данных сотрудника.
    /// </summary>
    public class UpdateEmployeeQuery : IRequest<UpdateEmployeeQueryResponse>
    {
        /// <summary>
        /// Предоставляет данные сотрудника.
        /// </summary>
        public EmployeeDto Employee { get; init; }
    }
}