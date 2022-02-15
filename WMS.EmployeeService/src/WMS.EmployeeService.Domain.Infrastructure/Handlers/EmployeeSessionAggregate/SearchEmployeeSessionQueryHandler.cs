using MediatR;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;

namespace WMS.EmployeeService.Domain.Infrastructure.Handlers.EmployeeSessionAggregate
{
    /// <summary>
    /// Представляет обработчик команды поиска данных сессии.
    /// </summary>
    public class SearchEmployeeSessionQueryHandler : IRequestHandler<SearchEmployeeSessionQuery, SearchEmployeeSessionQueryResponse>
    {
        /// <summary>
        /// Экземпляр репозитория сессий.
        /// </summary>
        private readonly IEmployeeSessionRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="SearchEmployeeSessionQueryHandler"/>.
        /// </summary>
        public SearchEmployeeSessionQueryHandler(IEmployeeSessionRepository repository) => _repository = repository;

        /// <summary>
        /// Обрабатывает команду поиска данных сессии.
        /// </summary>
        public async Task<SearchEmployeeSessionQueryResponse> Handle(SearchEmployeeSessionQuery request, CancellationToken cancellationToken)
        {
            EmployeeSession session = await _repository.GetById(request.SessionId, cancellationToken);

            return new SearchEmployeeSessionQueryResponse
            {
                Session = EmployeeSessionMapper.EntityToDto(session)
            };
        }
    }
}