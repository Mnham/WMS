using WMS.Microservice.Domain.Models;

namespace WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate
{
    /// <summary>
    /// Представляет сессию.
    /// </summary>
    public class EmployeeSession : Entity
    {
        /// <summary>
        /// Идентификатор работника.
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
            EmployeeId = employeeId;
            TaskTypeId = taskTypeId;
            EquipmentId = equipmentId;
        }
    }
}
