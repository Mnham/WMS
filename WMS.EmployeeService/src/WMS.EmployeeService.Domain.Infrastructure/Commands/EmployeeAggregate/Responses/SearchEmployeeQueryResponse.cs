using WMS.EmployeeService.Domain.Infrastructure.Models;
using WMS.Microservice.Infrastructure.Models;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate.Responses
{
    /// <summary>
    /// Предоставляет ответ на запрос поиска сотрудника.
    /// </summary>
    public class SearchEmployeeQueryResponse : IItemsModel<EmployeeDto>
    {
        /// <summary>
        /// Предоставляет или устанавливает коллекцию данных найденных сотрудников.
        /// </summary>
        public IReadOnlyList<EmployeeDto> Items { get; set; }
    }
}