using WMS.EmployeeService.Domain.Infrastructure.Models;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate.Responses
{
    public class SearchEmployeeSessionQueryResponse
    {
        public EmployeeSessionDto Session { get; init; }
    }
}