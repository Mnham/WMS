namespace WMS.EmployeeService.Domain.Infrastructure.Models
{
    /// <summary>
    /// Представляет данные сессии.
    /// </summary>
    public class EmployeeSessionDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор работника.
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// Идентифкатор типа задачи.
        /// </summary>
        public long TaskTypeId { get; set; }

        /// <summary>
        /// Идентифкатор оборудования.
        /// </summary>
        public long EquipmentId { get; set; }
    }
}