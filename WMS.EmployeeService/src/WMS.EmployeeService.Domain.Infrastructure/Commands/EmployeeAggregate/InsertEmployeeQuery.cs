using MediatR;

using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Models;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate
{
    public class InsertEmployeeQuery : IRequest<InsertEmployeeQueryResponse>
    {
        public EmployeeDto Employee { get; set; }
    }
}
