using MediatR;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Models;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate
{
    public class UpdateEmployeeQuery : IRequest<UpdateEmployeeQueryResponse>
    {
        public EmployeeDto Employee { get; set; }
    }
}
