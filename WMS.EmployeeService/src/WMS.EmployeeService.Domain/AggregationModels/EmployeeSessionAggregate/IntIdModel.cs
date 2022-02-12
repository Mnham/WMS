using WMS.Microservice.Domain.Infrastructure.Repositories.Infrastructure.Attributes;

namespace WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate
{
    public class IntIdModel
    {
        [SqlField("employee_session.id"), SqlOperator("=")]
        public long Id { get; init; }
    }
}