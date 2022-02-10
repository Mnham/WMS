namespace WMS.EmployeeService.Domain.Infrastructure.Models
{
    public class EmployeeSessionDto
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public long TaskTypeId { get; set; }
        public long EquipmentId { get; set; }
    }
}