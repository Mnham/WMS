using WMS.EmployeeService.Domain.Exceptions;
using WMS.Microservice.Domain.Models;

namespace WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate
{
    /// <summary>
    /// Представляет сессию.
    /// </summary>
    public class EmployeeSession : Entity
    {
        /// <summary>
        /// Идентификатор сотрудника.
        /// </summary>
        public long EmployeeId { get; private set; }

        /// <summary>
        /// Идентификатор типа задачи.
        /// </summary>
        public long TaskTypeId { get; private set; }

        /// <summary>
        /// Идентификатор оборудования.
        /// </summary>
        public long EquipmentId { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="EmployeeSession"/>.
        /// </summary>
        public EmployeeSession(long id, long employeeId, long taskTypeId, long equipmentId)
        {
            Id = id;
            SetEmployeeId(employeeId);
            SetTaskTypeId(taskTypeId);
            SetEquipmentId(equipmentId);
        }

        /// <summary>
        /// Устанавливает идентификатор сотрудника.
        /// </summary>
        public void SetEmployeeId(long id)
        {
            if (id <= 0)
            {
                throw new NegativeOrZeroValueException(nameof(id));
            }

            EmployeeId = id;
        }

        /// <summary>
        /// Устанавливает идентификатор типа задачи.
        /// </summary>
        public void SetTaskTypeId(long id)
        {
            if (id <= 0)
            {
                throw new NegativeOrZeroValueException(nameof(id));
            }

            TaskTypeId = id;
        }

        /// <summary>
        /// Устанавливает идентификатор оборудования.
        /// </summary>
        public void SetEquipmentId(long id)
        {
            if (id <= 0)
            {
                throw new NegativeOrZeroValueException(nameof(id));
            }

            EquipmentId = id;
        }
    }
}