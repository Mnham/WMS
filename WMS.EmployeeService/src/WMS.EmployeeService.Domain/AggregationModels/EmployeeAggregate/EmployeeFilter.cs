using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Attributes;

namespace WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate
{
    /// <summary>
    /// Представляет параметры фильтра поиска данных сотрудника.
    /// </summary>
    public class EmployeeFilter
    {
        /// <summary>
        /// Предоставляет или утсанавливает идентификатор сотрудника.
        /// </summary>
        [SqlField("employee.id"), SqlOperator("=")]
        public long? EmployeeId { get; set; }

        /// <summary>
        /// Предоставляет или устанавливает имя сотрудника.
        /// </summary>

        [SqlField("employee.name"), SqlOperator("LIKE")]
        public string EmployeeName { get; set; }
    }
}