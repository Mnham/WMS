using MediatR;
using WMS.ClassLibrary.Extensions;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;

namespace WMS.EmployeeService.Domain.Infrastructure.Handlers.EmployeeAggregate
{
    public class SearchEmployeeQueryHandler : IRequestHandler<SearchEmployeeQuery, SearchEmployeeQueryResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public SearchEmployeeQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<SearchEmployeeQueryResponse> Handle(SearchEmployeeQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<Employee> employees = await _employeeRepository.Search(
                new EmployeeFilter
                {
                    EmployeeId = request.EmployeeId,
                    EmployeeName = request.EmployeeName
                }, cancellationToken);

            return new SearchEmployeeQueryResponse
            {
                Items = employees.Map(EmployeeMapper.EntityToDto).ToList()
            };
        }
    }
}
