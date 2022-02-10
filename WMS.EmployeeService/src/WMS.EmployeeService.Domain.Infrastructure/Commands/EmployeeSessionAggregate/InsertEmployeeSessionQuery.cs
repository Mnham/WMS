using MediatR;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Models;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate
{
    public class InsertEmployeeSessionQuery : IRequest<InsertEmployeeSessionQueryResponse>
    {
        public EmployeeSessionDto Session { get; init; }
    }
}