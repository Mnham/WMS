using MediatR;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;

namespace WMS.EmployeeService.Domain.Infrastructure.Handlers.EmployeeSessionAggregate
{
    /// <summary>
    /// Представляет обработчик команды обновления сессии.
    /// </summary>
    public class UpdateEmployeeSessionQueryHandler : IRequestHandler<UpdateEmployeeSessionQuery, UpdateEmployeeSessionQueryResponse>
    {
        /// <summary>
        /// Экземпляр репозитория сессий.
        /// </summary>
        private readonly IEmployeeSessionRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="UpdateEmployeeSessionQueryHandler"/>.
        /// </summary>
        public UpdateEmployeeSessionQueryHandler(IEmployeeSessionRepository repository) => _repository = repository;

        /// <summary>
        /// Обрабатывает команду обновления сессии.
        /// </summary>
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