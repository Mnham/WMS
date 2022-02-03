using MediatR;

using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate.Responses;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate
{
    /// <summary>
    /// Представляет команду поиска сотрудника.
    /// </summary>
    public class SearchEmployeeQuery : IRequest<SearchEmployeeQueryResponse>
    {
        /// <summary>
        /// Предоставляет или устанавливает идентификатор сотрудника.
        /// </summary>
        public long? EmployeeId { get; set; }

        /// <summary>
        /// Предоставляет или устанавливает имя сотрудника.
        /// </summary>
        public string EmployeeName { get; set; }
    }
}