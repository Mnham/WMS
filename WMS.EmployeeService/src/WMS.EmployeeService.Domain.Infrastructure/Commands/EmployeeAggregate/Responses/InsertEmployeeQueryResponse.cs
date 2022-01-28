using WMS.EmployeeService.Domain.Infrastructure.Models;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate.Responses
{
    public class InsertEmployeeQueryResponse
    {
        public EmployeeDto Employee { get; set; }
    }
}
