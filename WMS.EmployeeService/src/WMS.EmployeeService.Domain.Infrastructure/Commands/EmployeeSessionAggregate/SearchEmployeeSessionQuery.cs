using MediatR;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate.Responses;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate
{
    public class SearchEmployeeSessionQuery : IRequest<SearchEmployeeSessionQueryResponse>
    {
        public long SessionId { get; set; }
    }
}