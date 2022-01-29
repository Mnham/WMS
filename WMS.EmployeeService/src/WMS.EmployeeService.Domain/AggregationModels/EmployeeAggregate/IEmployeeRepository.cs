namespace WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate
{
    public interface IEmployeeRepository
    {
        Task<Employee> Insert(Employee employee, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<Employee>> Search(EmployeeFilter request, CancellationToken cancellationToken);

        Task<Employee> Update(Employee employee, CancellationToken cancellationToken);
    }
}
