using WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate;

namespace WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate
{
    public interface IEmployeeSessionRepository
    {
        /// <summary>
        /// Выполняет добавление данных сотрудника в базу данных.
        /// </summary>
        public Task<EmployeeSession> Insert(EmployeeSession itemToInsert, CancellationToken cancellationToken);

        /// <summary>
        /// Выполняет поиск данных сотрудника в базе данных.
        /// </summary>
        public Task<EmployeeSession> GetById(IntIdModel id, CancellationToken cancellationToken);

        /// <summary>
        /// Выполняет обновление данных сотрудника в базу данных.
        /// </summary>
        public Task<EmployeeSession> Update(EmployeeSession itemToUpdate, CancellationToken cancellationToken);
    }
}