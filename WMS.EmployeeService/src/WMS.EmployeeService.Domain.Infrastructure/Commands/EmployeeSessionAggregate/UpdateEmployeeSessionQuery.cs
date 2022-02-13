using MediatR;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Models;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate
{
    /// <summary>
    /// Представляет команду для запроса обновления данных сессии.
    /// </summary>
    public class UpdateEmployeeSessionQuery : IRequest<UpdateEmployeeSessionQueryResponse>
    {
        /// <summary>
        /// Сессия.
        /// </summary>
        public EmployeeSessionDto Session { get; init; }
    }
}