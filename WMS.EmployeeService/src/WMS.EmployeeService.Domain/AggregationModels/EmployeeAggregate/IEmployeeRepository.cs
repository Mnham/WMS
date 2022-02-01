namespace WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate
{
    /// <summary>
    /// Определяет интерфейс репозитория данных пользователей.
    /// </summary>
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Выполняет добавление данных сотрудника в базу данных.
        /// </summary>
        public Task<Employee> Insert(Employee itemToInsert, CancellationToken cancellationToken);

        /// <summary>
        /// Выполняет поиск данных сотрудника в базе данных.
        /// </summary>
        public Task<IReadOnlyCollection<Employee>> Search(EmployeeFilter filter, CancellationToken cancellationToken);

        /// <summary>
        /// Выполняет обновление данных сотрудника в базу данных.
        /// </summary>
        public Task<Employee> Update(Employee itemToUpdate, CancellationToken cancellationToken);
    }
}