using WMS.EmployeeService.Domain.Infrastructure.Models;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate.Responses
{
    /// <summary>
    /// Представляет ответ на запрос добавления сессии.
    /// </summary>
    public class InsertEmployeeSessionQueryResponse
    {
        /// <summary>
        /// Сессия.
        /// </summary>
        public EmployeeSessionDto Session { get; init; }
    }
}