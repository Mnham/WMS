using WMS.EmployeeService.Domain.Infrastructure.Models;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate.Responses
{
    /// <summary>
    /// Представляет ответ на запрос обновления данных сотрудника.
    /// </summary>
    public class UpdateEmployeeQueryResponse
    {
        /// <summary>
        /// Предоставляет обновленные данные сотрудника.
        /// </summary>
        public EmployeeDto Employee { get; init; }
    }
}