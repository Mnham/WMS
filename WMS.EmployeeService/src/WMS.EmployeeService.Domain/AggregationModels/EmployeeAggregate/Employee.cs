using WMS.ClassLibrary.Domain.Models;

namespace WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate
{
    public class Employee : Entity
    {
        public Employee(long id, string name)
        {
            Id = id;
            SetName(name);
        }

        public string Name { get; private set; }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" не может быть пустым или содержать только пробел.", nameof(name));
            }

            Name = name;
        }
    }
}
