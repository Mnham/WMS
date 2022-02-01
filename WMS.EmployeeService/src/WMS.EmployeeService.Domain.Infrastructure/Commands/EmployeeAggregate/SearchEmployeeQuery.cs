using MediatR;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate.Responses;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate
{
    public class SearchEmployeeQuery : IRequest<SearchEmployeeQueryResponse>
    {
        public long? EmployeeId { get; set; }

        public string EmployeeName { get; set; }
    }
}
