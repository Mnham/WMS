namespace WMS.EmployeeService.Domain.Infrastructure.Models
{
    /// <summary>
    /// Представляет данные сотркдника.
    /// </summary>
    public sealed class EmployeeDto
    {
        /// <summary>
        /// Предоставляет или утсанавливает идентификатор.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Предоставляет или устанавливает имя сотрудника.
        /// </summary>
        public string Name { get; set; }
    }
}