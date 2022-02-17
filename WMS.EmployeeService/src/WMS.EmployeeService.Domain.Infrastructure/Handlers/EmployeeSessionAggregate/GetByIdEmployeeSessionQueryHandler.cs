using MediatR;

using WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;

namespace WMS.EmployeeService.Domain.Infrastructure.Handlers.EmployeeSessionAggregate
{
    /// <summary>
    /// Представляет обработчик команды запроса сессии.
    /// </summary>
    public class GetByIdEmployeeSessionQueryHandler : IRequestHandler<GetByIdEmployeeSessionQuery, GetByIdEmployeeSessionQueryResponse>
    {
        /// <summary>
        /// Экземпляр репозитория сессий.
        /// </summary>
        private readonly IEmployeeSessionRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="GetByIdEmployeeSessionQueryHandler"/>.
        /// </summary>
        public GetByIdEmployeeSessionQueryHandler(IEmployeeSessionRepository repository) => _repository = repository;

        /// <summary>
        /// Обрабатывает команду поиска сессии.
        /// </summary>
        public async Task<GetByIdEmployeeSessionQueryResponse> Handle(GetByIdEmployeeSessionQuery request, CancellationToken cancellationToken)
        {
            EmployeeSession session = await _repository.GetById(request.SessionId, cancellationToken);

            return new GetByIdEmployeeSessionQueryResponse
            {
                Session = EmployeeSessionMapper.EntityToDto(session)
            };
        }
    }
}
