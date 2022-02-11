using MediatR;

using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Models;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate
{
    /// <summary>
    /// Предоставляет команду добавления сотрудника.
    /// </summary>
    public class InsertEmployeeQuery : IRequest<InsertEmployeeQueryResponse>
    {
        /// <summary>
        /// Предоставляет или устанавливает данные сотрудника.
        /// </summary>
        public EmployeeDto Employee { get; init; }
    }
}