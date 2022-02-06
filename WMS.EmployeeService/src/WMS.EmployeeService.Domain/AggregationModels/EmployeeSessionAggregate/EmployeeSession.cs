using WMS.ClassLibrary.Domain.Models;
using WMS.EmployeeService.Domain.Exceptions;

namespace WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate
{
    public class EmployeeSession : Entity
    {
        public long EmployeeId { get; private set; }
        public long TaskTypeId { get; private set; }
        public long EquipmentId { get; private set; }

        public EmployeeSession(long id, long employeeId, long taskTypeId, long equipmentId)
        {
            Id = id;
            SetEmployeeId(employeeId);
            SetTaskTypeId(taskTypeId);
            SetEquipmentId(equipmentId);
        }

        public void SetEmployeeId(long id)
        {
            if (id <= 0)
            {
                throw new NegativeOrZeroValueException(nameof(id));
            }

            EmployeeId = id;
        }

        public void SetTaskTypeId(long id)
        {
            if (id <= 0)
            {
                throw new NegativeOrZeroValueException(nameof(id));
            }

            TaskTypeId = id;
        }

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