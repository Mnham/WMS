using WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Models;
using WMS.EmployeeService.Grpc;

namespace WMS.EmployeeService.Domain.Infrastructure.Helpers
{
    /// <summary>
    /// Предоставляет методы мапинга данных сотрудника.
    /// </summary>
    public static class EmployeeMapper
    {
        /// <summary>
        /// Выполняет преобразование экземпляра <see cref="EmployeeDto"/> в экземпляр <see cref="EmployeeGrpc"/>.
        /// </summary>
        public static EmployeeGrpc DtoToGrpc(EmployeeDto item) => new()
        {
            Id = item.Id,
            Name = item.Name
        };

        /// <summary>
        /// Выполняет преобразование экземпляра <see cref="EmployeeGrpc"/> в экземпляр <see cref="EmployeeDto"/>.
        /// </summary>
        public static EmployeeDto GrpcToDto(EmployeeGrpc item) => new()
        {
            Id = item.Id,
            Name = item.Name,
        };

        /// <summary>
        /// Выполняет преобразование экземпляра <see cref="Employee"/> в экземпляр <see cref="EmployeeDto"/>.
        /// </summary>
        public static EmployeeDto EntityToDto(Employee item) => new()
        {
            Id = item.Id,
            Name = item.Name,
        };

        /// <summary>
        /// Выполняет преобразование экземпляра <see cref="EmployeeDto"/> в экземпляр <see cref="Employee"/>.
        /// </summary>
        public static Employee DtoToEntity(EmployeeDto item) => new
        (
            item.Id,
            item.Name
        );
    }
}