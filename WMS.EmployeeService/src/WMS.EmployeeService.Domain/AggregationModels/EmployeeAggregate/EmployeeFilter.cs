using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Attributes;

namespace WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate
{
    /// <summary>
    /// Представляет параметры фильтра поиска сотрудника.
    /// </summary>
    public class EmployeeFilter
    {
        /// <summary>
        /// Идентификатор сотрудника.
        /// </summary>
        [SqlField("employee.id"), SqlOperator("=")]
        public long? EmployeeId { get; set; }

        /// <summary>
        /// Имя сотрудника.
        /// </summary>

        [SqlField("employee.name"), SqlOperator("LIKE")]
        public string EmployeeName { get; set; }
    }
}