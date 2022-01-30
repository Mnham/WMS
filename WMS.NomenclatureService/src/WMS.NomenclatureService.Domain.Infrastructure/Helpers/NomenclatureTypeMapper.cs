using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureTypeAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Models;
using WMS.NomenclatureService.Grpc;

namespace WMS.NomenclatureService.Domain.Infrastructure.Helpers
{
    /// <summary>
    /// Представляет методы маппинга типа номенклатуры.
    /// </summary>
    public static class NomenclatureTypeMapper
    {
        /// <summary>
        /// Возвращает экземпляр Grpc, преобразованный из Dto.
        /// </summary>
        public static NomenclatureTypeGrpc DtoToGrpc(NomenclatureTypeDto item) => new()
        {
            Id = item.Id,
            Name = item.Name
        };

        /// <summary>
        /// Возвращает экземпляр Dto, преобразованный из Grpc.
        /// </summary>
        public static NomenclatureTypeDto GrpcToDto(NomenclatureTypeGrpc item) => new()
        {
            Id = item.Id,
            Name = item.Name
        };

        /// <summary>
        /// Возвращает экземпляр Dto, преобразованный из Entity.
        /// </summary>
        public static NomenclatureTypeDto EntityToDto(NomenclatureType item) => new()
        {
            Id = item.Id,
            Name = item.Name
        };

        /// <summary>
        /// Возвращает экземпляр Entity, преобразованный из Dto.
        /// </summary>
        internal static NomenclatureType DtoToEntity(NomenclatureTypeDto item) => new
        (
            item.Id,
            item.Name
        );
    }
}