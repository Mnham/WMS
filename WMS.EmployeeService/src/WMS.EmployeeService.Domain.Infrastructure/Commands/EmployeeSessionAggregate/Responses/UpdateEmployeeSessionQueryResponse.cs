using WMS.EmployeeService.Domain.Infrastructure.Models;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate.Responses
{
    public class UpdateEmployeeSessionQueryResponse
    {
        public EmployeeSessionDto Session { get; init; }
    }
}