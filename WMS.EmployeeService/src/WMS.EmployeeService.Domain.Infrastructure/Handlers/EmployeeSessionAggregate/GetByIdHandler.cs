using MediatR;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;

namespace WMS.EmployeeService.Domain.Infrastructure.Handlers.EmployeeSessionAggregate
{
    /// <summary>
    /// Представляет обработчик команды поиска сессии.
    /// </summary>
    public class GetByIdHandler : IRequestHandler<GetById, GetByIdResponse>
    {
        /// <summary>
        /// Экземпляр репозитория сессий.
        /// </summary>
        private readonly IEmployeeSessionRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="GetByIdHandler"/>.
        /// </summary>
        public GetByIdHandler(IEmployeeSessionRepository repository) => _repository = repository;

        /// <summary>
        /// Обрабатывает команду поиска сессии.
        /// </summary>
        public async Task<GetByIdResponse> Handle(GetById request, CancellationToken cancellationToken)
        {
            EmployeeSession session = await _repository.GetById(request.SessionId, cancellationToken);

            return new GetByIdResponse
            {
                Session = EmployeeSessionMapper.EntityToDto(session)
            };
        }
    }
}