using WMS.EmployeeService.Domain.Infrastructure.Models;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate.Responses
{
    /// <summary>
    /// Предоставляет ответ на запрос добавления сотрудника.
    /// </summary>
    public class InsertEmployeeQueryResponse
    {
        /// <summary>
        /// Предоставляет данные сотрудника.
        /// </summary>
        public EmployeeDto Employee { get; init; }
    }
}