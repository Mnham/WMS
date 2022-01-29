using WMS.EmployeeService.Domain.AggregationModels.EmployeeAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Models;
using WMS.EmployeeService.Grpc;

namespace WMS.EmployeeService.Domain.Infrastructure.Helpers
{
    public static class EmployeeMapper
    {
        public static EmployeeGrpc DtoToGrpc(EmployeeDto item) => new()
        {
            Id = item.Id,
            Name = item.Name
        };

        public static EmployeeDto GrpcToDto(EmployeeGrpc item) => new()
        {
            Id = item.Id,
            Name = item.Name,
        };

        public static EmployeeDto EntityToDto(Employee item) => new()
        {
            Id = item.Id,
            Name = item.Name,
        };

        public static Employee DtoToEntity(EmployeeDto item) => new
        (
            item.Id,
            item.Name
        );
    }
}