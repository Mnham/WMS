using MediatR;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;

namespace WMS.EmployeeService.Domain.Infrastructure.Handlers.EmployeeAggregate
{
    public class UpdateEmployeeQueryHandler : IRequestHandler<UpdateEmployeeQuery, UpdateEmployeeQueryResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateEmployeeQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<UpdateEmployeeQueryResponse> Handle(UpdateEmployeeQuery request, CancellationToken cancellationToken)
        {
            Employee employee = await _employeeRepository.Update(
                EmployeeMapper.DtoToEntity(request.Employee),
                cancellationToken);

            return new UpdateEmployeeQueryResponse
            {
                Employee = EmployeeMapper.EntityToDto(employee)
            };
        }
    }
}
