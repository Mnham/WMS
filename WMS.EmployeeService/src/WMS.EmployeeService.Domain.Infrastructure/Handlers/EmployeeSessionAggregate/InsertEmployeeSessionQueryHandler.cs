using MediatR;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;

namespace WMS.EmployeeService.Domain.Infrastructure.Handlers.EmployeeSessionAggregate
{
    public class InsertEmployeeSessionQueryHandler : IRequestHandler<InsertEmployeeSessionQuery, InsertEmployeeSessionQueryResponse>
    {
        private readonly IEmployeeSessionRepository _repository;

        public InsertEmployeeSessionQueryHandler(IEmployeeSessionRepository repository) => _repository = repository;

        public async Task<InsertEmployeeSessionQueryResponse> Handle(InsertEmployeeSessionQuery request, CancellationToken cancellationToken)
        {
            EmployeeSession session = await _repository.Insert(
                EmployeeSessionMapper.DtoToEntity(request.Session),
                cancellationToken);

            return new InsertEmployeeSessionQueryResponse
            {
                Session = EmployeeSessionMapper.EntityToDto(session)
            };
        }
    }
}