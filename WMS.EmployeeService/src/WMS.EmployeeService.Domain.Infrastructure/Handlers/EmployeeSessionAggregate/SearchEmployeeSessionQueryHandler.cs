using MediatR;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;

namespace WMS.EmployeeService.Domain.Infrastructure.Handlers.EmployeeSessionAggregate
{
    public class SearchEmployeeSessionQueryHandler : IRequestHandler<SearchEmployeeSessionQuery, SearchEmployeeSessionQueryResponse>
    {
        private readonly IEmployeeSessionRepository _repository;

        public SearchEmployeeSessionQueryHandler(IEmployeeSessionRepository repository) => _repository = repository;

        public async Task<SearchEmployeeSessionQueryResponse> Handle(SearchEmployeeSessionQuery request, CancellationToken cancellationToken)
        {
            EmployeeSession session = await _repository.GetById(
                new IntIdModel
                {
                    Id = request.SessionId
                }, cancellationToken);

            return new SearchEmployeeSessionQueryResponse
            {
                Session = EmployeeSessionMapper.EntityToDto(session)
            };
        }
    }
}