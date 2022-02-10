using MediatR;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;

namespace WMS.EmployeeService.Domain.Infrastructure.Handlers.EmployeeSessionAggregate
{
    public class UpdateEmployeeSessionQueryHandler : IRequestHandler<UpdateEmployeeSessionQuery, UpdateEmployeeSessionQueryResponse>
    {
        private readonly IEmployeeSessionRepository _repository;

        public UpdateEmployeeSessionQueryHandler(IEmployeeSessionRepository repository) => _repository = repository;

        public async Task<UpdateEmployeeSessionQueryResponse> Handle(UpdateEmployeeSessionQuery request, CancellationToken cancellationToken)
        {
            EmployeeSession session = await _repository.Update(
                EmployeeSessionMapper.DtoToEntity(request.Session),
                cancellationToken);

            return new UpdateEmployeeSessionQueryResponse
            {
                Session = EmployeeSessionMapper.EntityToDto(session)
            };
        }
    }
}