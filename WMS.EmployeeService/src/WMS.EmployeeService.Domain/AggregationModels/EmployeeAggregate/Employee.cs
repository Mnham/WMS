using WMS.Microservice.Domain.Models;

namespace WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate
{
    /// <summary>
    /// Представляет данные сотрудника.
    /// </summary>
    public class Employee : Entity
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="Employee"/>.
        /// </summary>
        public Employee(long id, string name)
        {
            Id = id;
            SetName(name);
        }

        /// <summary>
        /// Имя сотрудника.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Устанавливает имя сотрудника.
        /// </summary>
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