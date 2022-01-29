using MediatR;

using WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;

namespace WMS.EmployeeService.Domain.Infrastructure.Handlers.EmployeeAggregate
{
    public class InsertEmployeeQueryHandler : IRequestHandler<InsertEmployeeQuery, InsertEmployeeQueryResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public InsertEmployeeQueryHandler(IEmployeeRepository employeeRepository) =>
            _employeeRepository = employeeRepository;

        public async Task<InsertEmployeeQueryResponse> Handle(InsertEmployeeQuery request, CancellationToken cancellationToken)
        {
            Employee employee = await _employeeRepository.Insert(
                EmployeeMapper.DtoToEntity(request.Employee),
                cancellationToken);

            return new InsertEmployeeQueryResponse()
            {
                Employee = EmployeeMapper.EntityToDto(employee)
            };
        }
    }
}
