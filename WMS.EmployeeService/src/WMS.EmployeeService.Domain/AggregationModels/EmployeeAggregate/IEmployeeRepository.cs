namespace WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate
{
    public interface IEmployeeRepository
    {
        Task<Employee> Insert(Employee employee, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<Employee>> Search(EmployeeFilter filter, CancellationToken cancellationToken);

        Task<Employee> Update(Employee itemToUpdate, CancellationToken cancellationToken);
    }
}
