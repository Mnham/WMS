using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Attributes;

namespace WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate
{
    public class EmployeeFilter
    {
        [SqlField("employee.id"), SqlOperator("=")]
        public long? EmployeeId { get; set; }

        [SqlField("employee.name"), SqlOperator("LIKE")]
        public string EmployeeName { get; set; }
    }
}