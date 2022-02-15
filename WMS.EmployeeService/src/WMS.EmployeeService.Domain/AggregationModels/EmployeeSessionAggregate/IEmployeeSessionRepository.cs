namespace WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate
{
    /// <summary>
    /// Интерфейс репозитория сессий.
    /// </summary>
    public interface IEmployeeSessionRepository
    {
        /// <summary>
        /// Добавляет сессию.
        /// </summary>
        public Task<EmployeeSession> Insert(EmployeeSession itemToInsert, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает сессию по идентификатору.
        /// </summary>
        public Task<EmployeeSession> GetById(long id, CancellationToken cancellationToken);

        /// <summary>
        /// Обновлет сессию.
        /// </summary>
        public Task<EmployeeSession> Update(EmployeeSession itemToUpdate, CancellationToken cancellationToken);
    }
}
