using WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Models;
using WMS.EmployeeService.Grpc;

namespace WMS.EmployeeService.Domain.Infrastructure.Helpers
{
    /// <summary>
    /// Предоставляет методы мапинга данных сессии.
    /// </summary>
    public class EmployeeSessionMapper
    {
        /// <summary>
        /// Выполняет преобразование экземпляра <see cref="EmployeeSessionDto"/> в экземпляр <see cref="EmployeeSessionGrpc"/>.
        /// </summary>
        public static EmployeeSessionGrpc DtoToGrpc(EmployeeSessionDto item) => new()
        {
            Id = item.Id,
            EmployeeId = item.EmployeeId,
            EquipmentId = item.EquipmentId,
            TaskTypeId = item.TaskTypeId
        };

        /// <summary>
        /// Выполняет преобразование экземпляра <see cref="EmployeeSessionGrpc"/> в экземпляр <see cref="EmployeeSessionDto"/>.
        /// </summary>
        public static EmployeeSessionDto GrpcToDto(EmployeeSessionGrpc item) => new()
        {
            Id = item.Id,
            EmployeeId = item.EmployeeId,
            EquipmentId = item.EquipmentId,
            TaskTypeId = item.TaskTypeId
        };

        /// <summary>
        /// Выполняет преобразование экземпляра <see cref="EmployeeSession"/> в экземпляр <see cref="EmployeeSessionDto"/>.
        /// </summary>
        public static EmployeeSessionDto EntityToDto(EmployeeSession item) => new()
        {
            Id = item.Id,
            EmployeeId = item.EmployeeId,
            EquipmentId = item.EquipmentId,
            TaskTypeId = item.TaskTypeId
        };

        /// <summary>
        /// Выполняет преобразование экземпляра <see cref="EmployeeSessionDto"/> в экземпляр <see cref="EmployeeSession"/>.
        /// </summary>
        public static EmployeeSession DtoToEntity(EmployeeSessionDto item) => new
        (
            item.Id,
            item.EmployeeId,
            item.TaskTypeId,
            item.EquipmentId
        );
    }
}