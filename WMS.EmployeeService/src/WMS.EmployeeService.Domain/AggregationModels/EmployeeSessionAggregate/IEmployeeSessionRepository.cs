namespace WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate
{
    /// <summary>
    /// Представляет репозиторий сессий.
    /// </summary>
    public interface IEmployeeSessionRepository
    {
        /// <summary>
        /// Выполняет добавление данных сессии в базу данных.
        /// </summary>
        public Task<EmployeeSession> Insert(EmployeeSession itemToInsert, CancellationToken cancellationToken);

        /// <summary>
        /// Выполняет поиск данных сессии в базе данных.
        /// </summary>
        public Task<EmployeeSession> GetById(IntIdModel id, CancellationToken cancellationToken);

        /// <summary>
        /// Выполняет обновление данных сессии в базу данных.
        /// </summary>
        public Task<EmployeeSession> Update(EmployeeSession itemToUpdate, CancellationToken cancellationToken);
    }
}