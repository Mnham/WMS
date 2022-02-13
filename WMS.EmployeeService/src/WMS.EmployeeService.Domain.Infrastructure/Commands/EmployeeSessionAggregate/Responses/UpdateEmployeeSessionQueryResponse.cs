using WMS.EmployeeService.Domain.Infrastructure.Models;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate.Responses
{
    /// <summary>
    /// Представляет ответ на запрос обновления сессии.
    /// </summary>
    public class UpdateEmployeeSessionQueryResponse
    {
        /// <summary>
        /// Сессия.
        /// </summary>
        public EmployeeSessionDto Session { get; init; }
    }
}