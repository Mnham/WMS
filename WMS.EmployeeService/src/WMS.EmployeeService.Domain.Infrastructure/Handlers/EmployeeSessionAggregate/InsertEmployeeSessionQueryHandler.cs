using MediatR;
using WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;

namespace WMS.EmployeeService.Domain.Infrastructure.Handlers.EmployeeSessionAggregate
{
    /// <summary>
    /// Представляет обработчик команды добавления сессии.
    /// </summary>
    public class InsertEmployeeSessionQueryHandler : IRequestHandler<InsertEmployeeSessionQuery, InsertEmployeeSessionQueryResponse>
    {
        /// <summary>
        /// Экземпляр репозитория сессий.
        /// </summary>
        private readonly IEmployeeSessionRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="InsertEmployeeSessionQueryHandler"/>.
        /// </summary>
        public InsertEmployeeSessionQueryHandler(IEmployeeSessionRepository repository) => _repository = repository;

        /// <summary>
        /// Обрабатывает команду добавления сессии.
        /// </summary>
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