using WMS.EmployeeService.Domain.AggregationModels.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Models;
using WMS.EmployeeService.Grpc;

namespace WMS.EmployeeService.Domain.Infrastructure.Helpers
{
    /// <summary>
    /// Предоставляет методы мапинга сессии.
    /// </summary>
    public class EmployeeSessionMapper
    {
        /// <summary>
        /// Возвращает экземпляр Grpc, преобразованный из Dto.
        /// </summary>
        public static EmployeeSessionGrpc DtoToGrpc(EmployeeSessionDto item) => new()
        {
            Id = item.Id,
            EmployeeId = item.EmployeeId,
            EquipmentId = item.EquipmentId,
            TaskTypeId = item.TaskTypeId
        };

        /// <summary>
        /// Возвращает экземпляр Dto, преобразованный из Grpc.
        /// </summary>
        public static EmployeeSessionDto GrpcToDto(EmployeeSessionGrpc item) => new()
        {
            Id = item.Id,
            EmployeeId = item.EmployeeId,
            EquipmentId = item.EquipmentId,
            TaskTypeId = item.TaskTypeId
        };

        /// <summary>
        /// Возвращает экземпляр Dto, преобразованный из Entity.
        /// </summary>
        public static EmployeeSessionDto EntityToDto(EmployeeSession item) => new()
        {
            Id = item.Id,
            EmployeeId = item.EmployeeId,
            EquipmentId = item.EquipmentId,
            TaskTypeId = item.TaskTypeId
        };

        /// <summary>
        /// Возвращает экземпляр Entity, преобразованный из Dto.
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
