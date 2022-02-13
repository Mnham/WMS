using WMS.Microservice.Domain.Infrastructure.Repositories.Infrastructure.Attributes;

namespace WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate
{
    /// <summary>
    /// Представляет фильтр поиска сессии.
    /// </summary>
    public class IntIdModel
    {
        /// <summary>
        /// Идентификатор сессии.
        /// </summary>
        [SqlField("employee_session.id"), SqlOperator("=")]
        public long Id { get; init; }
    }
}