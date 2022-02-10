using MediatR;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Models;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate
{
    public class UpdateEmployeeSessionQuery : IRequest<UpdateEmployeeSessionQueryResponse>
    {
        public EmployeeSessionDto Session { get; init; }
    }
}